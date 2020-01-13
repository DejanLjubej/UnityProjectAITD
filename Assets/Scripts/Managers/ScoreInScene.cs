using UnityEngine.UI;
using UnityEngine;

public class ScoreInScene : MonoBehaviour {

    public Text enemiesDestroyedText;
    public static int enemiesDestroyed;

    public Text lootGatheredText;
    public static int lootGathered;

    int startScore =0;

    // Use this for initialization

    // Update is called once per frame

    private void Start()
    {
        enemiesDestroyed = startScore;
        lootGathered = startScore;
    }

    void Update () {
        enemiesDestroyedText.text = enemiesDestroyed.ToString();

        lootGatheredText.text = lootGathered.ToString();
	}

   
}
