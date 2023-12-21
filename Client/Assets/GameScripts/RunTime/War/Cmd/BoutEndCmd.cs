namespace GameScripts.RunTime.War
{
    public class BoutEndCmd : WarCmd
    {
        protected override void OnExecute()
        {
            WarManager.Current.BoutEnd();
        }
    }
}