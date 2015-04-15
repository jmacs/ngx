namespace NgxLib.Maps
{
    public class MapObject
    {
        public string Prefab { get; private set; }
        public PrefabArgs Args { get; private set; }

        public MapObject(string prefab, PrefabArgs args)
        {
            Prefab = prefab;
            Args = args;
        }
    }
}
