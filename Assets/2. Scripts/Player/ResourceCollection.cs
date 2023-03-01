namespace Core {
    public class ResourceCollection : ResourceAmount {
        public new float this[ResourceType r] {
            get {
                return base[r];
            }
            set {
                if (r == ResourceType.green) {
                    green = value;
                    return;
                }
                if (r == ResourceType.red) {
                    red = value;
                    return;
                }
                if (r == ResourceType.blue) {
                    blue = value;
                    return;
                }
                if (r == ResourceType.yellow) {
                    yellow = value;
                    return;
                }
                if (r == ResourceType.diamond) {
                    diamond = value;
                    return;
                }

                throw new System.ArgumentException($"INVALID RESOURCE TYPE: '{r}'");
            }
        }

        public void Reset() {
            green = 0;
            red = 0;
            blue = 0;
            yellow = 0;
            diamond = 0;
        }

        public void Add(ResourceAmount r) {
            green += r[ResourceType.green];
            red += r[ResourceType.red];
            blue += r[ResourceType.blue];
            yellow += r[ResourceType.yellow];
            diamond += r[ResourceType.diamond];
        }
        public void Spend(ResourceAmount cost) {
            green -= cost[ResourceType.green];
            red -= cost[ResourceType.red];
            blue -= cost[ResourceType.blue];
            yellow -= cost[ResourceType.yellow];
            diamond -= cost[ResourceType.diamond];
        }
    }
}
