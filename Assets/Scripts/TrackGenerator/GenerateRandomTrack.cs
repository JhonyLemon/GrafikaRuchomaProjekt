using System.Collections.Generic;
using UnityEngine;

public class StartingPoint
{
    public int x { get; set; }
    public int y { get; set; }

    public StartingPoint(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
}

public class GenerateRandomTrack : MonoBehaviour
{
    List<GameObject> trackElements = new List<GameObject>();
    public GameObject straightPrefab;
    public GameObject turnPrefab;
    public Vector3 vector = new Vector3(0, 0, 0);
    public Direction actualDirection = Direction.UP;
    public bool lastTrackWasStraight;
    public bool lastTurnWasRight;

    //"tracks" is an array that represents template of our track.
    //"0" in array represents empty space for track, and 1 represents that field is taken.
    //In the beggining of this script all of array field will be filled with 0;
    public int[,] tracks = new int[99, 99];
    public StartingPoint lastTrackXY = new StartingPoint(50, 50);

    public class VectorAndQuaternion
    {
        public Vector3 vector { get; set; }
        public Quaternion quaternion { get; set; }

        public GameObject prefab { get; set; }

        public VectorAndQuaternion(GameObject prefab, Vector3 vector, Quaternion quaternion)
        {
            this.prefab = prefab;
            this.vector = vector;
            this.quaternion = quaternion;
        }
    }

    public enum Direction
    {
        UP,
        DOWN,
        LEFT,
        RIGHT
    }

    // Start is called before the first frame update
    void Start()
    {
        //Initialize array with 0
        for (int i = 0; i < 31; i++)
        {
            for (int j = 0; j < 31; j++)
            {
                tracks[i, j] = 0;
            }
        }

        lastTrackWasStraight = true;

        //Start with initializing straight track element
        GameObject firstTrackElement = Instantiate(straightPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        firstTrackElement.transform.localScale = new Vector3(35, 35, 35);
        trackElements.Add(firstTrackElement);
        tracks[lastTrackXY.x, lastTrackXY.y] = 1;

        /* Generating random number. 
          1 - turn left
          2 - straight
          3 - turn right
        */
        int turnOrStraight;
        for (int i = 0; i < 50; i++)
        {
            do
            {
                turnOrStraight = Random.Range(0, 100);
            } while (!Verification(actualDirection, turnOrStraight));
            AddNewTrack(actualDirection, turnOrStraight);
        }
    }

    public bool Verification(Direction direction, int turnOrStraight)
    {
        switch (direction)
        {
            case Direction.UP:
                if (turnOrStraight <= 30)
                {
                    if (tracks[lastTrackXY.x - 1, lastTrackXY.y - 1] == 1 || tracks[lastTrackXY.x - 2, lastTrackXY.y - 1] == 1)
                        return false;
                }
                else if (turnOrStraight > 30 && turnOrStraight < 70)
                {
                    if (tracks[lastTrackXY.x, lastTrackXY.y - 1] == 1 || tracks[lastTrackXY.x, lastTrackXY.y - 2] == 1)
                        return false;
                }
                else if (turnOrStraight >= 70)
                {
                    if (tracks[lastTrackXY.x + 1, lastTrackXY.y - 1] == 1 || tracks[lastTrackXY.x + 2, lastTrackXY.y - 1] == 1)
                        return false;
                }
                break;
            case Direction.DOWN:
                if (turnOrStraight <= 30)
                {
                    if (tracks[lastTrackXY.x + 1, lastTrackXY.y + 1] == 1 || tracks[lastTrackXY.x + 2, lastTrackXY.y + 1] == 1)
                        return false;
                }
                else if (turnOrStraight > 30 && turnOrStraight < 70)
                {
                    if (tracks[lastTrackXY.x, lastTrackXY.y + 1] == 1 || tracks[lastTrackXY.x, lastTrackXY.y + 2] == 1)
                        return false;
                }
                else if (turnOrStraight >= 70)
                {
                    if (tracks[lastTrackXY.x - 1, lastTrackXY.y + 1] == 1 || tracks[lastTrackXY.x - 2, lastTrackXY.y + 1] == 1)
                        return false;
                }
                break;
            case Direction.LEFT:
                if (turnOrStraight <= 30)
                {
                    if (tracks[lastTrackXY.x - 1, lastTrackXY.y + 1] == 1 || tracks[lastTrackXY.x - 1, lastTrackXY.y + 2] == 1)
                        return false;

                }
                else if (turnOrStraight > 30 && turnOrStraight < 70)
                {
                    if (tracks[lastTrackXY.x - 1, lastTrackXY.y] == 1 || tracks[lastTrackXY.x - 2, lastTrackXY.y] == 1)
                        return false;
                }
                else if (turnOrStraight >= 70)
                {
                    if (tracks[lastTrackXY.x - 1, lastTrackXY.y - 2] == 1 || tracks[lastTrackXY.x - 1, lastTrackXY.y - 1] == 1)
                        return false;
                }
                break;
            case Direction.RIGHT:
                if (turnOrStraight <= 30)
                {
                    if (tracks[lastTrackXY.x + 1, lastTrackXY.y - 1] == 1 || tracks[lastTrackXY.x + 1, lastTrackXY.y - 2] == 1)
                        return false;
                }
                else if (turnOrStraight > 30 && turnOrStraight < 70)
                {
                    if (tracks[lastTrackXY.x + 1, lastTrackXY.y] == 1 || tracks[lastTrackXY.x + 2, lastTrackXY.y] == 1)
                        return false;
                }
                else if (turnOrStraight >= 70)
                {
                    if (tracks[lastTrackXY.x + 1, lastTrackXY.y + 1] == 1 || tracks[lastTrackXY.x + 1, lastTrackXY.y + 2] == 1)
                        return false;
                }
                break;
        }
        return true;
    }

    void AddNewTrack(Direction direction, int turnOrStraight)
    {
        GameObject newTrackElement;
        VectorAndQuaternion vectorAndQuaternion = null;
        switch (direction)
        {
            case Direction.UP:
                if (turnOrStraight <= 30)
                {
                    if (lastTrackWasStraight)
                        vectorAndQuaternion = LastTrackWasStraight(turnOrStraight);
                    else if (lastTurnWasRight)
                    {
                        vectorAndQuaternion = new VectorAndQuaternion(turnPrefab, vector += new Vector3(35, 0, 0), Quaternion.identity);
                    }
                    else
                        vectorAndQuaternion = new VectorAndQuaternion(turnPrefab, vector += new Vector3(70, 0, 70), Quaternion.identity);

                    lastTrackWasStraight = false;
                    lastTurnWasRight = false;
                    actualDirection = Direction.LEFT;
                    lastTrackXY.y--;
                    tracks[lastTrackXY.x, lastTrackXY.y] = 1;
                }
                else if (turnOrStraight > 30 && turnOrStraight < 70)
                {
                    if (lastTrackWasStraight)
                    {
                        vectorAndQuaternion = new VectorAndQuaternion(straightPrefab, vector += new Vector3(0, 0, 70), Quaternion.identity);
                    }
                    else if (lastTurnWasRight)
                    {
                        vectorAndQuaternion = new VectorAndQuaternion(straightPrefab, vector += new Vector3(35, 0, 0), Quaternion.identity);
                    }
                    else
                        vectorAndQuaternion = new VectorAndQuaternion(straightPrefab, vector += new Vector3(70, 0, 70), Quaternion.identity);

                    lastTrackWasStraight = true;
                    lastTurnWasRight = false;
                    lastTrackXY.y--;
                    tracks[lastTrackXY.x, lastTrackXY.y] = 1;
                }
                else if (turnOrStraight >= 70)
                {
                    if (lastTrackWasStraight)
                        vectorAndQuaternion = LastTrackWasStraight(turnOrStraight);
                    else if (lastTurnWasRight)
                    {
                        vectorAndQuaternion = new VectorAndQuaternion(turnPrefab, vector += new Vector3(70, 0, 70), Quaternion.Euler(0, 270, 0));
                    }
                    else
                        vectorAndQuaternion = new VectorAndQuaternion(turnPrefab, vector += new Vector3(105, 0, 140), Quaternion.Euler(0, 270, 0));

                    lastTurnWasRight = true;
                    lastTrackWasStraight = false;
                    actualDirection = Direction.RIGHT;
                    lastTrackXY.y--;
                    tracks[lastTrackXY.x, lastTrackXY.y] = 1;
                }
                break;
            case Direction.DOWN:
                if (turnOrStraight <= 30)
                {
                    if (lastTrackWasStraight)
                        vectorAndQuaternion = LastTrackWasStraight(turnOrStraight);
                    else if (lastTurnWasRight)
                    {
                        vectorAndQuaternion = new VectorAndQuaternion(turnPrefab, vector += new Vector3(-35, 0, 0), Quaternion.Euler(0, 180, 0));
                    }
                    else
                        vectorAndQuaternion = new VectorAndQuaternion(turnPrefab, vector += new Vector3(-70, 0, -70), Quaternion.Euler(0, 180, 0));

                    lastTrackWasStraight = false;
                    lastTurnWasRight = false;
                    actualDirection = Direction.RIGHT;
                    lastTrackXY.y++;
                    tracks[lastTrackXY.x, lastTrackXY.y] = 1;
                }
                else if (turnOrStraight > 30 && turnOrStraight < 70)
                {
                    if (lastTrackWasStraight)
                        vectorAndQuaternion = LastTrackWasStraight(turnOrStraight);
                    else if (lastTurnWasRight)
                    {
                        vectorAndQuaternion = new VectorAndQuaternion(straightPrefab, vector += new Vector3(0, 0, -70), Quaternion.identity);
                    }
                    else
                        vectorAndQuaternion = new VectorAndQuaternion(straightPrefab, vector += new Vector3(-35, 0, -140), Quaternion.identity);
                    lastTrackWasStraight = true;
                    lastTurnWasRight = false;
                    lastTrackXY.y++;
                    tracks[lastTrackXY.x, lastTrackXY.y] = 1;
                }
                else if (turnOrStraight >= 70)
                {
                    if (lastTrackWasStraight)
                        vectorAndQuaternion = LastTrackWasStraight(turnOrStraight);
                    else if (lastTurnWasRight)
                    {

                    }
                    else
                        vectorAndQuaternion = new VectorAndQuaternion(turnPrefab, vector += new Vector3(-105, 0, -140), Quaternion.Euler(0, 90, 0));

                    lastTrackWasStraight = false;
                    lastTurnWasRight = true;
                    actualDirection = Direction.LEFT;
                    lastTrackXY.y++;
                    tracks[lastTrackXY.x, lastTrackXY.y] = 1;
                }
                break;
            case Direction.LEFT:
                if (turnOrStraight <= 30)
                {
                    if (lastTrackWasStraight)
                        vectorAndQuaternion = LastTrackWasStraight(turnOrStraight);
                    else if (lastTurnWasRight)
                    {
                        vectorAndQuaternion = new VectorAndQuaternion(turnPrefab, vector += new Vector3(0, 0, 35), Quaternion.Euler(0, 270, 0));
                    }
                    else
                        vectorAndQuaternion = new VectorAndQuaternion(turnPrefab, vector += new Vector3(-70, 0, 70), Quaternion.Euler(0, 270, 0));

                    lastTrackWasStraight = false;
                    lastTurnWasRight = false;
                    actualDirection = Direction.DOWN;
                    lastTrackXY.x--;
                    tracks[lastTrackXY.x, lastTrackXY.y] = 1;
                }
                else if (turnOrStraight > 30 && turnOrStraight < 70)
                {
                    if (lastTrackWasStraight)
                        vectorAndQuaternion = LastTrackWasStraight(turnOrStraight);
                    else if (lastTurnWasRight)
                    {
                        vectorAndQuaternion = new VectorAndQuaternion(straightPrefab, vector += new Vector3(-70, 0, 0), Quaternion.Euler(0, 90, 0));
                    }
                    else
                        vectorAndQuaternion = new VectorAndQuaternion(straightPrefab, vector += new Vector3(-140, 0, 35), Quaternion.Euler(0, 90, 0));

                    lastTurnWasRight = false;
                    lastTrackWasStraight = true;
                    lastTrackXY.x--;
                    tracks[lastTrackXY.x, lastTrackXY.y] = 1;
                }
                else if (turnOrStraight >= 70)
                {
                    if (lastTrackWasStraight)
                        vectorAndQuaternion = LastTrackWasStraight(turnOrStraight);
                    else if (lastTurnWasRight)
                    {
                        vectorAndQuaternion = new VectorAndQuaternion(turnPrefab, vector += new Vector3(-70, 0, 70), Quaternion.Euler(0, 180, 0));
                    }
                    else
                        vectorAndQuaternion = new VectorAndQuaternion(turnPrefab, vector += new Vector3(-140, 0, 105), Quaternion.Euler(0, 180, 0));

                    lastTrackWasStraight = false;
                    lastTurnWasRight = true;
                    actualDirection = Direction.UP;
                    lastTrackXY.x--;
                    tracks[lastTrackXY.x, lastTrackXY.y] = 1;
                }
                break;
            case Direction.RIGHT:
                if (turnOrStraight <= 30)
                {
                    if (lastTrackWasStraight)
                        vectorAndQuaternion = LastTrackWasStraight(turnOrStraight);
                    else if (lastTurnWasRight)
                    {
                        vectorAndQuaternion = new VectorAndQuaternion(turnPrefab, vector += new Vector3(0, 0, -35), Quaternion.Euler(0, 90, 0));
                    }
                    else
                        vectorAndQuaternion = new VectorAndQuaternion(turnPrefab, vector += new Vector3(70, 0, -70), Quaternion.Euler(0, 90, 0));

                    lastTrackWasStraight = false;
                    lastTurnWasRight = false;
                    actualDirection = Direction.UP;
                    lastTrackXY.x++;
                    tracks[lastTrackXY.x, lastTrackXY.y] = 1;
                }
                else if (turnOrStraight > 30 && turnOrStraight < 70)
                {
                    if (lastTrackWasStraight)
                        vectorAndQuaternion = LastTrackWasStraight(turnOrStraight);
                    else if (lastTurnWasRight)
                    {
                        vectorAndQuaternion = new VectorAndQuaternion(straightPrefab, vector += new Vector3(0, 0, -35), Quaternion.Euler(0, 90, 0));
                    }
                    else
                        vectorAndQuaternion = new VectorAndQuaternion(straightPrefab, vector += new Vector3(70, 0, -70), Quaternion.Euler(0, 90, 0));

                    lastTurnWasRight = false;
                    lastTrackWasStraight = true;
                    lastTrackXY.x++;
                    tracks[lastTrackXY.x, lastTrackXY.y] = 1;
                }
                else if (turnOrStraight >= 70)
                {
                    if (lastTrackWasStraight)
                        vectorAndQuaternion = LastTrackWasStraight(turnOrStraight);
                    else if (lastTurnWasRight)
                    {
                        vectorAndQuaternion = new VectorAndQuaternion(turnPrefab, vector += new Vector3(70, 0, -70), Quaternion.identity);
                    }
                    else
                        vectorAndQuaternion = new VectorAndQuaternion(turnPrefab, vector += new Vector3(140, 0, -105), Quaternion.identity);

                    lastTrackWasStraight = false;
                    lastTurnWasRight = true;
                    actualDirection = Direction.DOWN;
                    lastTrackXY.x++;
                    tracks[lastTrackXY.x, lastTrackXY.y] = 1;
                }
                break;
        }
        newTrackElement = Instantiate(vectorAndQuaternion.prefab, vectorAndQuaternion.vector, vectorAndQuaternion.quaternion);
        newTrackElement.transform.localScale = new Vector3(35, 35, 35);
        trackElements.Add(newTrackElement);
    }

    VectorAndQuaternion LastTrackWasStraight(int turnOrStraight)
    {
        VectorAndQuaternion vectorAndQuaternion = null;
        switch (actualDirection)
        {
            case Direction.UP:
                {
                    if (turnOrStraight <= 30)
                    {
                        vectorAndQuaternion = new VectorAndQuaternion(turnPrefab, vector += new Vector3(0, 0, 70), Quaternion.identity);
                    }
                    else if (turnOrStraight >= 70)
                    {
                        vectorAndQuaternion = new VectorAndQuaternion(turnPrefab, vector += new Vector3(35, 0, 140), Quaternion.Euler(0, 270, 0));
                    }
                    break;
                }
            case Direction.DOWN:
                {
                    if (turnOrStraight <= 30)
                    {
                        vectorAndQuaternion = new VectorAndQuaternion(turnPrefab, vector += new Vector3(-35, 0, 0), Quaternion.Euler(0, 180, 0));
                    }
                    else if (turnOrStraight > 30 && turnOrStraight < 70)
                    {
                        vectorAndQuaternion = new VectorAndQuaternion(straightPrefab, vector += new Vector3(0, 0, -70), Quaternion.identity);
                    }
                    else if (turnOrStraight >= 70)
                    {
                        vectorAndQuaternion = new VectorAndQuaternion(turnPrefab, vector += new Vector3(-70, 0, -70), Quaternion.Euler(0, 90, 0));
                    }
                    break;
                }
            case Direction.LEFT:
                {
                    if (turnOrStraight <= 30)
                    {
                        vectorAndQuaternion = new VectorAndQuaternion(turnPrefab, vector += new Vector3(0, 0, 35), Quaternion.Euler(0, 270, 0));
                    }
                    else if (turnOrStraight > 30 && turnOrStraight < 70)
                    {
                        vectorAndQuaternion = new VectorAndQuaternion(straightPrefab, vector += new Vector3(-70, 0, 0), Quaternion.Euler(0, 90, 0));
                    }
                    else if (turnOrStraight >= 70)
                    {
                        vectorAndQuaternion = new VectorAndQuaternion(turnPrefab, vector += new Vector3(-70, 0, 70), Quaternion.Euler(0, 180, 0));
                    }
                    break;
                }
            case Direction.RIGHT:
                {
                    if (turnOrStraight <= 30)
                    {
                        vectorAndQuaternion = new VectorAndQuaternion(turnPrefab, vector += new Vector3(70, 0, 0), Quaternion.Euler(0, 90, 0));
                    }
                    else if (turnOrStraight > 30 && turnOrStraight < 70)
                    {
                        vectorAndQuaternion = new VectorAndQuaternion(straightPrefab, vector += new Vector3(70, 0, 0), Quaternion.Euler(0, 90, 0));
                    }
                    else if (turnOrStraight >= 70)
                    {
                        vectorAndQuaternion = new VectorAndQuaternion(turnPrefab, vector += new Vector3(140, 0, -35), Quaternion.identity);
                    }
                    break;
                }
        }
        return vectorAndQuaternion;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
