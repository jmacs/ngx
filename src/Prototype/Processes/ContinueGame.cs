using NgxLib;
using NgxLib.Processing;
using Prototype.User;

namespace Prototype.Processes
{
    public class ContinueGame : ProcessModule
    {
        protected string ProfileName { get; set; }

        public ContinueGame(string profileName)
        {
            ProfileName = profileName;
            Root = new Sequence(
                new Task(LoadProfile),
                new Task(StartFromLastLocation)
                );
        }

        protected ProcessStatus LoadProfile()
        {
            // TODO: load profile from file
            var profile = GetDummyProfile();
            Runtime.Session["profile"] = profile;
            return ProcessStatus.Success;
        }

        protected ProcessStatus StartFromLastLocation()
        {
            var profile = Runtime.Session["profile"] as Profile;
            var proc = new EnterStage(profile.QuestLog.LastStage);
            //var proc = new EnterWorldProcess(profile.QuestLog.LastWorld);
            Runtime.ProcessManager.Start(proc);
            return ProcessStatus.Success;
        }

        private Profile GetDummyProfile()
        {
            var profile = new Profile();
            profile.Name = "Bomb.com";
            profile.QuestLog.LastWorld = 9000;
            profile.QuestLog.LastStage = 1001;
            return profile;
        }
    }
}
