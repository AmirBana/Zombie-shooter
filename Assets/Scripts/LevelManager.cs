using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct enemyCount
{
    public int enemy1;
    public int enemy2;
    public int enemy3;
    public int enemy4;
}
public class LevelManager : MonoBehaviour
{
    public List<enemyCount> level;

}
