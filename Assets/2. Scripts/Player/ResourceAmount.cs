namespace Core {
    public class ResourceAmount {
        public float this[ResourceType r] {
            get {
                if (r == ResourceType.green) {
                    return green;
                }
                if (r == ResourceType.red) {
                    return red;
                }
                if (r == ResourceType.blue) {
                    return blue;
                }
                if (r == ResourceType.yellow) {
                    return yellow;
                }
                if (r == ResourceType.diamond) {
                    return diamond;
                }

                throw new System.ArgumentException($"INVALID RESOURCE TYPE: '{r}'");
            }
        }
       
        protected float green;
        protected float red;
        protected float blue;
        protected float yellow;
        protected float diamond;


        public ResourceAmount(float green = 0, float red = 0, float blue = 0, float yellow = 0, float diamond = 0) {
            this.green = green;
            this.red = red;
            this.blue = blue;
            this.yellow = yellow;
            this.diamond = diamond;
        }

        /// <summary>
        /// Does this contain at least the resources specified in req
        /// </summary>
        public bool Satisfies(ResourceAmount req) {
            return
                green >= req.green &&
                red >= req.red &&
                blue >= req.blue &&
                yellow >= req.yellow &&
                diamond >= req.diamond;
        }
    }
}
