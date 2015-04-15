
namespace Prototype
{
    // Message Keys
    public static class Msg
    {
        // triggers

        public const int Swap_Sprite_Layer = 10000;
        public const int Play_Sound = 10001;
        public const int Disable_Sound = 10002;

        // events

        public const int On_Player_touch_Pickup = 20000;
        public const int On_Player_touch_Interactor = 20001;
        public const int On_Player_touch_Projectile = 20002;
        public const int On_JumpBoots_touch_Shell = 20003;
        public const int On_JumpBoots_touch_Stompable = 20004;

    }

    // Game controls
    public class Ctrl
    {
        public const int None = 0;
        public const int Pause = 1;
        public const int Menu = 2;
        public const int Up = 4;
        public const int Down = 8;
        public const int Left = 16;
        public const int Right = 32;
        public const int Run = 64;
        public const int Jump = 128;
        public const int Action1 = 256;
        public const int Action2 = 512;
    }

    // Pick-up Items
    public static class Item
    {
        public const int Mushroom = 1;
    }

    // Components 1 to 1,023
    public static class Component
    {
        public const int Ducker = 28;
        public const int NullPower = 27;
        public const int SuperPower = 26;
        public const int PickUp = 25;
        public const int Dispencer = 24;
        public const int SpawnPoint = 23;
        public const int StagePortal = 22;
        public const int MapLocale = 21;
        public const int WarpPipeConnection = 20;
        public const int Stompable = 19;
        public const int Shell = 18;
        public const int Projectile = 17;
        public const int Presence = 16;
        public const int CoinBag = 15;
        public const int OverworldNavigator = 14;
        public const int JumpBoots = 13;
        public const int Interactor = 12;
        public const int Mobility = 11;
        public const int Player = 10;
        public const int Brain = 9;
        public const int BlockDecorator = 8;
        public const int AudioPlayer = 7;
        public const int Controller = 6;
        public const int Sprite = 5;
        public const int Animator = 4;
        public const int Spatial = 2;
        public const int RigidBody = 3;
        public const int MetaData = 1;
    }

    // Tile ids from smb3-world.xml
    public static class WorldTile
    {
        public const int EmptyBlock = 406;
    }

    // Tile props
    public static class TileProperty
    {
        public const int Rail = 1;
        public const int RailStop = 2;
        public const int BrickBlock = 3;
        public const int CoinBlock = 4;
    }

    // Context names
    public static class Ctx
    {
        public const string Intro = "IntroContext";
        public const string Menu = "MenuContext";
        public const string Stage = "StageContext";
        public const string World = "WorldContext";
        public const string Test = "TestingContext";
    }

    // Spawn Point IDs
    public static class Spwn
    {
        public const int PlayerStart = 1;
    }

    // Sound clips
    public static class Snd
    {
        public const int AirshipClear = 100001;
        public const int AirshipMove = 100002;
        public const int BossSpit = 100003;
        public const int BowserFall = 100004;
        public const int BowserFire = 100005;
        public const int BreakBrick = 100006;
        public const int Bump = 100007;
        public const int Cherry = 100008;
        public const int Coin = 100009;
        public const int DoorAppears = 100010;
        public const int EnemyHit = 100011;
        public const int EnterLevel = 100012;
        public const int Fireball = 100013;
        public const int Flagpole = 100014;
        public const int FortressClear = 100015;
        public const int GameOver = 100016;
        public const int HammerBrosShuffle = 100017;
        public const int HurryUp = 100018;
        public const int InventoryOpenClose = 100019;
        public const int Jump = 100020;
        public const int JumpSmall = 100021;
        public const int Kick = 100022;
        public const int LazerBeam = 100023;
        public const int LevelClear = 100024;
        public const int LostSuit = 100025;
        public const int MapNewWorld = 100026;
        public const int MapTravel = 100027;
        public const int Match = 100028;
        public const int NoMatch = 100029;
        public const int OneUp = 100030;
        public const int Pause = 100031;
        public const int PlayerDown = 100032;
        public const int PMeter = 100033;
        public const int Powerup = 100034;
        public const int PowerupAppear = 100035;
        public const int Stomp = 100036;
        public const int Tail = 100037;
        public const int Text = 100038;
        public const int Throw = 100039;
        public const int Thwomp = 100040;
        public const int Transform = 100041;
        public const int Vine = 100042;
        public const int WarpPipe = 100043;
        public const int Whistle = 100044;
    }
}
