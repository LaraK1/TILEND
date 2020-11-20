using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.IO;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    int rows;
    int cols;
    string[,] levelData;

    [Header("Presets")]

    public GameObject parentLevel;
    public GameObject forest;
    public GameObject wild;
    public GameObject sand;
    public GameObject see;
    public GameObject deepSee;
    public GameObject mountain;
    public GameObject berry;
    public GameObject camp;
    public GameObject water;
    public GameObject build;

    [Header("Level objects")]

    public GameObject player;
    public GameObject parentLevelObject;
    public TextAsset levelTextFile;


    /// <summary>
    /// Starts to create level with the start of this.
    /// </summary>
    void Start()
    {
        ReadLevel(levelTextFile);
        CreateLevel();
    }


    /// <summary>
    /// Read txt level file and creats a level matrix.
    /// </summary>
    /// <param name="textFile">TextAsset with level data.</param>
    void ReadLevel(TextAsset textFile)
    {
        string[] lines = textFile.text.Split(new[] { '\r', '\n' }, System.StringSplitOptions.RemoveEmptyEntries); // split by new line, return
        string[] nums = lines[0].Split(new[] { '\t' }); // split by tab
        rows = lines.Length; // number of rows
        cols = nums.Length; // number of columns

        if(rows != 10 || cols != 6) // check if right size
        {
            Debug.LogError("invalid map - wrong size");
        }

        levelData = new string[rows, cols]; // level data matrix

        bool buildTile = false;

        // fill level data matrix
        for (int i = 0; i < rows; i++)
        {
            string st = lines[i];
            nums = st.Split(new[] { '\t' }); // split by tab

            for (int j = 0; j < cols; j++)
            {
                // valid map check - just one build tile
                if(nums[j] == "9")
                {
                    if (!buildTile)
                        buildTile = true;
                    else
                    {
                        Debug.LogError("invalid map - more than one build tile");
                    }
                }

                // fill level data
                levelData[i, j] = nums[j];
            }
        }
    }

    /// <summary>
    /// Reads level data and initates the creation.
    /// </summary>
    void CreateLevel()
    {
        // rotate vector
        string[,] ret = new string[cols, rows];

        for (int i = 0; i < cols; ++i)
        {
            for (int j = 0; j < rows; ++j)
            {
                ret[i, j] = levelData[rows - j - 1, i];
            }
        }

        // fill the world
        for (int i = 0; i < cols; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                string val = ret[i,j];
                switch (val)
                {
                    case "x":
                        break;
                    case "0"://forest
                        CreateLevelObject(forest, i, j);
                        break;
                    case "1"://wild
                        CreateLevelObject(wild, i, j);
                        break;
                    case "2"://sand
                        CreateLevelObject(sand, i, j);
                        break;
                    case "3"://see
                        CreateLevelObject(see, i, j);
                        break;
                    case "4"://deepSee
                        CreateLevelObject(deepSee, i, j);
                        break;
                    case "5"://mountain
                        CreateLevelObject(mountain, i, j);
                        break;
                    case "6"://berry
                        CreateLevelObject(berry, i, j);
                        break;
                    case "7"://camp with player
                        player.transform.position = new Vector3(i, j, 0);
                        CreateLevelObject(camp, i, j);
                        break;
                    case "8"://water
                        CreateLevelObject(water, i, j);
                        break;
                    case "9"://build
                        CreateLevelObject(build, i, j);
                        break;
                    default:
                        break;

                }
            }
        }
    }

    /// <summary>
    /// Creates level objects with prefabs gameobjects.
    /// </summary>
    /// <param name="whatObject">Which prefab / GameObject is to be cloned.</param>
    /// <param name="i">Column coordinate of the new tile position.</param>
    /// <param name="j">Row coordinate of the new tile position.</param>
    void CreateLevelObject(GameObject whatObject, int i, int j)
    {
        // clone object
        GameObject newObject = Instantiate(whatObject, new Vector3(i, j, 0), Quaternion.identity);
        // give a new partent - empty level
        newObject.transform.parent = parentLevelObject.transform;

        // lower rows are on top / higher sorting order for sprite
        SpriteRenderer renderer = newObject.transform.Find("Sprite").GetComponent<SpriteRenderer>();
        renderer.sortingOrder = rows-j;
    }

}
