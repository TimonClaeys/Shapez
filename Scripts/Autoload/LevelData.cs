using Godot;
using System;
using Godot.Collections;
using Newtonsoft.Json;

public class LevelData : Node
{
    public static int Level { get; set; }
    public static int BestScore { get; set; }
    public static int CurrentScore { get; set; }

    public static void Save()
    {
        string output =  JsonConvert.SerializeObject(new SaveData());

        File saveGame = new File();

        saveGame.Open("user://savegame.save", File.ModeFlags.Write);
        saveGame.StoreLine(output);
        saveGame.Close();
    }

    public static void Load()
    {

        try
        {
            File saveGame =  new File();

            saveGame.Open("user://savegame.save", File.ModeFlags.Read);
            string data = saveGame.GetLine();
            GD.Print(data);
            SaveData saveData = JsonConvert.DeserializeObject<SaveData>(data);

            Level = saveData.Level;
            BestScore = saveData.BestScore;

            if (Level == 0)
            {
                Level = 1;
            }
            GD.Print(Level);
        }
        catch (System.Exception ex)
        {
            
            GD.Print(ex);
        }
        
    }
    
}

public class SaveData
{
    public int Level { get; set; }
    public int BestScore { get; set; }

    public SaveData()
    {
        Level = LevelData.Level;
        BestScore = LevelData.BestScore;
    }
}
