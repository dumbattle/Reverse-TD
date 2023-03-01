namespace Core {
    public class ResourceType {
        public static ResourceType green { get; } = new ResourceType();
        public static ResourceType red { get; } = new ResourceType();
        public static ResourceType blue { get; } = new ResourceType();
        public static ResourceType yellow { get; } = new ResourceType();
        public static ResourceType diamond { get; } = new ResourceType();

        ResourceType() { }
    }
}
