namespace Prototype.User
{
    public class Profile
    {
        public string Name { get; set; }
        public UserOptions UserOptions { get; set; }
        public QuestLog QuestLog { get; set; }

        public Profile()
        {
            UserOptions = new UserOptions();
            QuestLog = new QuestLog();
        }
    }
}
