using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RoomTrigger : MonoBehaviour
{
    public int id = 0;
    public GameObject tipPanel;

    private bool splitTutorial = false;
    private int mergeTutorial = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerController playerController = other.GetComponent<PlayerController>();
        if (playerController)
        {
            playerController.roomId = id;
            PromptTutorial();
        }
    }

    private void PromptTutorial()
    {
        if (id == 1 && !splitTutorial) {
            tipPanel.SetActive(true);
            tipPanel.transform.GetChild(0).gameObject.GetComponent<Text>().text = "Press K to split";

            StartCoroutine(FadeOutTip());
            splitTutorial = true;
        }

        if (id == 3)
        {
            mergeTutorial++;

            if(mergeTutorial == 2)
            {
                tipPanel.SetActive(true);
                tipPanel.transform.GetChild(0).gameObject.GetComponent<Text>().text = "Press M to merge";

                StartCoroutine(FadeOutTip());
            }
        }

        if(id == 6 || id == 14)
        {
            GameObject.FindGameObjectWithTag("CameraFade").GetComponent<CameraFadeBehaviour>().FadeToBlack();
            StartCoroutine(LoadLevel(id == 6? "Level2" : "TheEnd"));
        }
    }

    private IEnumerator FadeOutTip()
    {
        yield return new WaitForSeconds(4.0f);
        tipPanel.SetActive(false);
    }

    private IEnumerator LoadLevel(string levelName)
    {
        yield return new WaitForSeconds(3.5f);
        SceneManager.LoadScene(levelName);
    }
}
