using System.Collections.Generic;
using UnityEngine;

public class GenerateRandomTrack : MonoBehaviour
{
    List<GameObject> trackElements = new List<GameObject>();
    public GameObject straightPrefab;
    public GameObject turnPrefab;
    public Vector3 vector = new Vector3(0, 0, 0);
    public Direction actualDirection = Direction.UP;
    public bool lastTrackWasStraight;
    public bool lastTurnWasRight;

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
        lastTrackWasStraight = true;
        //Start with initializing straight track element
        GameObject firstTrackElement = Instantiate(straightPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        firstTrackElement.transform.localScale = new Vector3(35, 35, 35);
        trackElements.Add(firstTrackElement);

        /* Generating random number. 
          1 - turn left
          2 - straight
          3 - turn right
        */
        for (int i = 0; i < 25; i++)
        {
            int turnOrStraight = Random.Range(0, 100);
            System.Console.WriteLine(turnOrStraight);
            AddNewTrack(actualDirection, turnOrStraight);
        }
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
