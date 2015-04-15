namespace NgxLib
{
    public class Ngx
    {
        public static NgxComponentConfiguration Components { get; private set; }
        public static NgxMessenger Messenger { get; private set; }
        public static NgxPrefabCollection Prefabs { get; private set; }
        public static NgxContextCollection Contexts { get; private set; }

        static Ngx()
        {
            Components = new NgxComponentConfiguration();
            Messenger = new NgxMessenger();
            Prefabs = new NgxPrefabCollection();
            Contexts = new NgxContextCollection();
        } 
    }
}