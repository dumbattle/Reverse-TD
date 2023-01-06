namespace Core {
    public static class ITowerExtensions {
        public static int GetConstraintID(this ITower t) {
            var bl = t.GetBottomLeft();
            return bl.x * 1000 + bl.y;
        }
    }
}
