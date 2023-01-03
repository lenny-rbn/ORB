using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using UnityEngine;

[Serializable]
public struct gatchaObject
{
    public string name;
    public string description;
    public bool unlocked;

    public gatchaObject(string name, string description)
    {
        this.name = name;
        this.description = description;
        unlocked = false;
    }
}

[Serializable]
public class Lists
{
    public int chestNB;

    public gatchaObject[] festiveObjects =
    {
        new gatchaObject( "Anarchy", "By GUILLET Mo" ),
        new gatchaObject( "At Long Last", "By LEFEBURE Tristan" ),
        new gatchaObject( "Binary Escape", "By LOMBARD Kylian" ),
        new gatchaObject( "Chasing Bird", "By MARTY Esteban" ),
        new gatchaObject( "Eurêka", "By GICQUEL Guillaume" ),
        new gatchaObject( "Free the Children", "By RICHARD Léonie" ),
        new gatchaObject( "FreeYourSoul", "By LUBER Pascal-matthieu" ),
        new gatchaObject( "Ghosting", "By OGULLUK Lena" ),
        new gatchaObject( "Hacking For Freedom", "By BORNET Matis" ),
        new gatchaObject( "Hazardous Space", "By BRUNET Maxime" ),
        new gatchaObject( "Hell Button", "By GUILLET Amaury" ),
        new gatchaObject( "Hero's Journey", "By NEGGAZ Moussa" ),
        new gatchaObject( "Mechanic Memories", "By DUPONT DE DINECHIN Vincent" ),
        new gatchaObject( "Peaceful Dreamer", "By SABATINI Anthony" ),
        new gatchaObject( "Pool Horizon", "By ROBERT Marian" ),
        new gatchaObject( "RailJail", "By LORIMY Omane" ),
        new gatchaObject( "Road To Freedom", "By HOTTIN Denis" ),
        new gatchaObject( "Ronin Redemption", "By DAVID Henri" ),
        new gatchaObject( "Skull Lantern", "By SELLIER Benjamin" ),
        new gatchaObject( "Space Dodge", "By BOUTONNAT Tom" ),
        new gatchaObject( "Stay Alive", "By DIBOUES Killian" ),
        new gatchaObject( "The Outsider", "By WISDORFF Baptiste" ),
        new gatchaObject( "Tiny Iron Souls", "By DOIZELET Solène" ),
        new gatchaObject( "Twin's Odyssey", "By SIMONIN Marie" ),
        new gatchaObject( "Voltige", "By SOARES Maxwel" ),
        new gatchaObject( "Wing of Freedom", "By MAILLY Théo" ),
        new gatchaObject( "Wunkong", "By TECHER Océan" )
    };

    public gatchaObject[] explosiveObjects =
    {
        new gatchaObject( "AlterEgo", "By VELOSO Hugo" ),
        new gatchaObject( "Buds", "By COURCELAUD Guillaume" ),
        new gatchaObject( "Carrouscape", "By SAINTILAN Loïc" ),
        new gatchaObject( "FreedomWare", "By LAHAYE Thomas" ),
        new gatchaObject( "Git", "Guitar Hero babe !" ),
        new gatchaObject( "Mini Drift", "By CLAUSS Ethis" ),
        new gatchaObject( "Office Manager", "By JABER Rayan" ),
        new gatchaObject( "Perspective Traveller", "BASSELIER-MARCHAL Axel" ),
        new gatchaObject( "Plushies", "GRANJON Raphaël" ),
        new gatchaObject( "Presidence", "By BOUNIOL Victor"),
        new gatchaObject( "Prison Wreck", "HUBERT Basile" ),
        new gatchaObject( "Witchcraft Doctor", "THIL Jérôme" ),
    };

    public gatchaObject[] devilishObjects =
    {
        new gatchaObject( "A Night Out", "By RODRIGUES PENTEADO" ),
        new gatchaObject( "Chainsawnicorn", "By LOUTREUIL Dante" ),
        new gatchaObject( "Free Blade Zero", "By SOUSSI Gabriel" ),
        new gatchaObject( "Grogu", "Nuggieeees Youhouuu !" ),
        new gatchaObject( "Juicy Battle", "By CARLUY Théo" ),
        new gatchaObject( "Roaaaarrr", "By dev Emma <3" ),
        new gatchaObject( "Snake", "By dev Matéo" ),
        new gatchaObject( "SpaceDriver", "By MENARD Isaac" ),
    };

    public gatchaObject[] insaneObjects =
    {
        new gatchaObject( "Free electron", "By URSULET Ahmadji" ),
        new gatchaObject( "Homme Musclé José", "By dev Lenny" ),
        new gatchaObject( "Colossal Robot", "By the dev team :D" ),
    };
}

public class GatchaLists : MonoBehaviour
{
    public static Lists lists;

    private static bool loaded;

    public static void Load()
    {
        if (!loaded)
        {
            try
            {
                FileStream stream = File.Open("GatchaSave.bin", FileMode.Open);

                try
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    lists = new Lists();
                    lists = (Lists)formatter.Deserialize(stream);
                    SaveStats.chestNB = lists.chestNB;
                }
                catch (SerializationException e)
                {
                    Console.WriteLine("Failed to load. Reason: " + e.Message);
                    throw;
                }
                finally
                {
                    stream.Close();
                }
            }
            catch (FileNotFoundException)
            {
                lists = new Lists();
                Console.WriteLine("Save not found, create new one");
            }

            loaded = true;
        }
    }

    public static void Save()
    {
        if (lists == null)
            Load();

        lists.chestNB = SaveStats.chestNB;
        FileStream stream = File.Open("GatchaSave.bin", FileMode.Create);
        BinaryFormatter formatter = new BinaryFormatter();
        try
        {
            formatter.Serialize(stream, lists);
        }
        catch (SerializationException e)
        {
            Console.WriteLine("Failed to save. Reason: " + e.Message);
        }
        finally
        {
            stream.Close();
        }
    }
}
