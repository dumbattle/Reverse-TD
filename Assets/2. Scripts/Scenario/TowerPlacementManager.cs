using System.Collections.Generic;
using UnityEngine;


namespace Core {
    public class TowerPlacementManager {
        TowerManager towerManager;
        List<Group> _groups = new List<Group>();
        ScenarioInstance s;


        public TowerPlacementManager (ScenarioInstance s, TowerManager towerManager) {
            this.towerManager = towerManager;
            this.s = s;
        }


        //***************************************************************************************************
        // public
        //***************************************************************************************************
        public void StartNewGroup(ITower tower, bool main = false) {
            var newGroup = new Group();
            newGroup.main = main;
            newGroup.members.Add(tower);

            // validate non overlap
            for (int x = 0; x < tower.Size; x++) {
                for (int y = 0; y < tower.Size; y++) {
                    foreach (var g in _groups) {
                        if (!TileAvailable(new Vector2Int(x, y) + tower.GetBottomLeft(), tower)) {
                            throw new System.InvalidOperationException("New group overlaps existing group");
                        }
                    }
                }
            }
            AddTowerToGroup(tower, newGroup);

            // add group
            _groups.Add(newGroup);
        }

        public ITower SpawnTowerRandom(TowerDefinition d) {
            if (d.size > 1) {
                throw new System.NotImplementedException("Cannot spawn tower with size greater than 1");
            }
            // select group
            Group g = null;
            float r = 0; // resovoiur sample

            foreach (var group in _groups) {
                if (group.main || !group.canExpand) {
                    continue;
                }
                r += 1;
                if (Random.value < 1 / r) {
                    g = group;
                }
            }
            if (g == null) {
                return null;
            }
            // select location
            r = 0; // resovoiur sample
            Vector2Int location = new Vector2Int(-1, -1);

            for (int i = g.expandOptions.Count - 1; i >= 0; i--) {
                var opt = g.expandOptions[i];
                bool available = TileAvailable(opt, null, g);

                if (!available) {
                    g.expandOptions.RemoveAt(i);
                    continue;
                }
                float w = 1;
                r += w;

                if (Random.value < w / r) {
                    location = opt;
                }
            }

            if (location == new Vector2Int(-1, -1)) {
                return SpawnTowerRandom(d);
            }
            // create tower
            var t = d.GetNewInstance(s, location);

            // add to group
            AddTowerToGroup(t, g);

            return t;
        }
        public ITower SpawnTowerDisconected(TowerDefinition d) {
            if (d.size > 1) {
                throw new System.NotImplementedException("Cannot spawn tower with size greater than 1");
            }
            // select group
            Group g = _groups[0];

            foreach (var group in _groups) {
                if (group.main) {
                    continue;
                }
                if (group.members.Count < g.members.Count) {
                    g = group;
                }
            }

            var tile = GetRandomGroupSpawnLocation();
            if (tile == new Vector2Int(-1,-1)) {
                return null;
            }

            var t = d.GetNewInstance(s, tile);

            // add to group
            AddTowerToGroup(t, g);

            return t;
        }

        /// <summary>
        /// TODO - Currently O(n^2), optimize?
        /// </summary>
        public Vector2Int GetRandomGroupSpawnLocation() {
            Vector2Int result = new Vector2Int(-1, -1);
            int r = 0;

            for (int x = 1; x < s.mapQuery.width - 1; x++) {
                for (int y = 1; y < s.mapQuery.height - 1; y++) {
                    if (TileAvailable(new Vector2Int(x, y), null)) {
                        r++;
                        if (Random.value < 1f / r) {
                            result = new Vector2Int(x, y);
                        }
                    }
                }
            }
            return result;
        }


        public void ReplaceTower(ITower old, ITower replaceWith) {
            foreach (var g in _groups) {
                if (g.members.Contains(old)) {
                    g.members.Remove(old);
                    g.members.Add(replaceWith);
                }
            }
        }

        //***************************************************************************************************
        // Helpers
        //***************************************************************************************************

        bool TileAvailable(Vector2Int tile, ITower ignoreTower, Group ignoreGroup = null) {
            // in range
            if (!s.mapQuery.ValidTowerRange(tile.x, tile.y)) {
                return false;
            }

            // Check tower
            var existing = towerManager.towers[tile.x, tile.y];

            if (existing != null && existing != ignoreTower) {
                return false;
            }

            // check other groups
            foreach (var g in _groups) {
                if (g == ignoreGroup) {
                    continue;
                }
                if (g.reservedTiles.Contains(tile)) {
                    return false;
                }
            }
            return true;
        }
        
        void AddTowerToGroup (ITower tower, Group g) {
            g.members.Add(tower);

            // add reserved tiles
            for (int x = -1; x < tower.Size + 1; x++) {
                for (int y = -1; y < tower.Size + 1; y++) {
                    var t = new Vector2Int(x, y) + tower.GetBottomLeft();
                    if (!g.reservedTiles.Contains(t)) {
                        g.reservedTiles.Add(t);
                    }
                }
            }

            for (int x = 0; x < tower.Size; x++) {
                for (int y = 0; y < tower.Size; y++) {
                    var t = new Vector2Int(x, y) + tower.GetBottomLeft();
                    g.expandOptions.Remove(t);
                }
            }

            // add expand options
            for (int i = 0; i < tower.Size; i++) {
                // left
                var lt = tower.GetBottomLeft() + new Vector2Int(-1, i);
                if (TileAvailable(lt, tower, g) && !g.expandOptions.Contains(lt)) {
                    g.expandOptions.Add(lt);
                }
                // right
                var rt = tower.GetTopRight() + new Vector2Int(1, -i);
                if (TileAvailable(rt, tower, g) && !g.expandOptions.Contains(rt)) {
                    g.expandOptions.Add(rt);
                }
                // top
                var tt = tower.GetTopRight() + new Vector2Int(-i, 1);
                if (TileAvailable(tt, tower, g) && !g.expandOptions.Contains(tt)) {
                    g.expandOptions.Add(tt);
                }
                // bottom
                var bt = tower.GetBottomLeft() + new Vector2Int(i, -1);
                if (TileAvailable(bt, tower, g) && !g.expandOptions.Contains(bt)) {
                    g.expandOptions.Add(bt);
                }
            }
        }


        class Group {
            public bool main;

            public HashSet<ITower> members = new HashSet<ITower>();

            /// <summary>
            /// Includes all tiles occupied by towers as well a 1 tile buffer
            /// </summary>
            public HashSet<Vector2Int> reservedTiles = new HashSet<Vector2Int>();
            public List<Vector2Int> expandOptions = new List<Vector2Int>();

            public bool canExpand => expandOptions.Count > 0;
        }   
    }
}
