public class SaveObject
{
    public bool Level1 = true;
    public bool Level2 = false;
    public bool Level3 = false;
    public bool Level4 = false;
    public bool Level5 = false;
    public bool Level6 = false;
    public bool Level7 = false;
    public bool CustomLevel = false;

    public void GiveAccess(string sceneName) 
    {
        switch (sceneName) 
        {
            case SceneNames.Level2:
                Level2 = true;
                break;

            case SceneNames.Level3:
                Level3 = true;
                break;

            case SceneNames.Level4:
                Level4 = true;
                break;
            case SceneNames.Level5:
                Level5 = true;
                break;

            case SceneNames.Level6:
                Level6 = true;
                break;

            case SceneNames.Level7:
                Level7 = true;
                break;

            case SceneNames.CustomLevel:
                CustomLevel = true;
                break;
        }   
    }
}