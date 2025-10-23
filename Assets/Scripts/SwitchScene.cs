using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScene : MonoBehaviour
{
    public bool choose_player_level = false;
    public int player_level;
    public string[] scenes;
    public Animator animator;
    public bool ending = false;
    public static void SwapScene(string scene_name){
        SceneManager.LoadScene(scene_name);
    }

    public void FadeBlack(){
        animator.SetTrigger("Fade");
    }

    public void SwatchScene(int scene_number){
        SceneManager.LoadScene(scenes[scene_number]);
    }

    void OnTriggerEnter(Collider collider){
        if (collider.CompareTag("Player")){
            if (choose_player_level){
                NewMovement.level = player_level;
            }
            if (ending){
                SwapScene("TrueEnd");
            }

            else{
                SwapScene("BranchScene");
            }
        }
    }

    public void SwitchScenes(string scene_name){
        SceneManager.LoadScene(scene_name);
    }
}
