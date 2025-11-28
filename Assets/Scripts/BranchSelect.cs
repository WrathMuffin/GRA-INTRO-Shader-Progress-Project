using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class BranchSelect : MonoBehaviour
{
    public float selection = -1;
    public Image solver_icon;
    public Animator solver_animator;
    public Vector2 level_1_pos;
    public Vector2 level_2_pos;
    public Vector2 level_3_pos;

    public GameObject[] level_1_branches;
    public GameObject[] level_2_branches;
    public GameObject[] level_3_branches;
    public TextBoxes text_boxes;
    public Material not_active_material;
    public Material active_material;
    public Material not_active_stencil_material;
    public Material active_stencil_material;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {

        if (NewMovement.level == 0){
            solver_animator.SetInteger("Level", 0);
        }

        else if (NewMovement.level == 1){
            solver_animator.SetInteger("Level", 1);
        }

        else if (NewMovement.level == 2){
            solver_animator.SetInteger("Level", 2);
        }

        else if (NewMovement.level == 3){
            solver_animator.SetInteger("Level", 3);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W)){
            if (NewMovement.level == 1){
                level_1_branches[0].GetComponent<MeshRenderer>().material = active_material;
                level_1_branches[1].GetComponent<MeshRenderer>().material = not_active_material;
                level_1_branches[2].GetComponent<MeshRenderer>().material = active_stencil_material;
                level_1_branches[3].GetComponent<MeshRenderer>().material = not_active_stencil_material;

                selection = 1.1f;
                text_boxes.StartTextBoxes(1);
            }
            
            else if (NewMovement.level == 2){
                level_2_branches[0].GetComponent<MeshRenderer>().material = active_material;
                level_2_branches[1].GetComponent<MeshRenderer>().material = not_active_material;
                level_2_branches[2].GetComponent<MeshRenderer>().material = active_stencil_material;
                level_2_branches[3].GetComponent<MeshRenderer>().material = not_active_stencil_material;
                //level_2_branches[2].GetComponent<Material>().SetColor("Base color", not_active_color);
                selection = 2.1f;
                text_boxes.StartTextBoxes(3);
            }

            else if (NewMovement.level == 3){
                level_3_branches[0].GetComponent<MeshRenderer>().material = active_material;
                level_3_branches[1].GetComponent<MeshRenderer>().material = not_active_material;
                level_3_branches[2].GetComponent<MeshRenderer>().material = active_stencil_material;
                level_3_branches[3].GetComponent<MeshRenderer>().material = not_active_stencil_material;
                //level_3_branches[2].GetComponent<Material>().SetColor("Base color", active_color);
                selection = 3.1f;
                text_boxes.StartTextBoxes(5);
            }
        }

        if (Input.GetKeyDown(KeyCode.S)){
            if (NewMovement.level == 1){
                level_1_branches[1].GetComponent<MeshRenderer>().material = active_material;
                level_1_branches[0].GetComponent<MeshRenderer>().material = not_active_material;
                level_1_branches[3].GetComponent<MeshRenderer>().material = active_stencil_material;
                level_1_branches[2].GetComponent<MeshRenderer>().material = not_active_stencil_material;
                selection = 1.2f;
                text_boxes.StartTextBoxes(2);
            }

            else if (NewMovement.level == 2){
                //level_2_branches[2].GetComponent<Material>().SetColor("Base color", active_color);
                level_2_branches[1].GetComponent<MeshRenderer>().material = active_material;
                level_2_branches[0].GetComponent<MeshRenderer>().material = not_active_material;
                level_2_branches[3].GetComponent<MeshRenderer>().material = active_stencil_material;
                level_2_branches[2].GetComponent<MeshRenderer>().material = not_active_stencil_material;
                selection = 2.2f;
                text_boxes.StartTextBoxes(4);
            }

            else if (NewMovement.level == 3){
                level_3_branches[0].GetComponent<MeshRenderer>().material = not_active_material;
                level_3_branches[1].GetComponent<MeshRenderer>().material = active_material;
                level_3_branches[3].GetComponent<MeshRenderer>().material = active_stencil_material;
                level_3_branches[2].GetComponent<MeshRenderer>().material = not_active_stencil_material;
                //level_3_branches[2].GetComponent<Material>().SetColor("Base color", not_active_color);
                selection = 3.2f;
                text_boxes.StartTextBoxes(6);
            }
        }

        if (Input.GetKeyDown(KeyCode.Space)){
            if (selection == 1.1f || selection == 2.1f || selection == 3.1f){
                solver_animator.SetInteger("Level", 4);
            }

            else if (selection == 1.2f || selection == 2.2f || selection == 3.2f){
                solver_animator.SetInteger("Level", 5);
            }
        }

        
    }

    
}
