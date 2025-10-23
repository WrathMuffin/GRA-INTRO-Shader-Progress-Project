using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System;
using Unity.VisualScripting;

public class TextBoxes : MonoBehaviour
{
	public TextMeshProUGUI text;
    public TextMeshProUGUI name_text;
    public string[] dialogue_lines;
    public Sprite[] dialogue_images;
    public string[] dialogue_names;
    public Image portrait;
    public float text_speed;
    private int index = 0;
    public static bool text_active = false;
    public bool multi_text = true;
    public bool character_sound = true;
    public AudioSource character_source;
    public bool freeze_time = false;
    public GameObject panel;
    public bool text_box = false;
    public AudioSource music_play;
    public bool play_music = true;
    public bool end;
    public GameObject end_object;
    public bool give_control = true;
    NewMovement player;

    void Start(){
        if (panel != null){
            panel.SetActive(false);
        }

        if (character_source == null){
            character_sound = false;
        }
        text.text = string.Empty;
        //  StartTextBoxes();
    }

    void Update(){
        if (text_box){
            name_text.text = dialogue_names[index];
            portrait.sprite = dialogue_images[index];
        }

        if (multi_text){
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.E)){
                if (text.text == dialogue_lines[index]){
                    NextLine();
                }

                else{
                    StopAllCoroutines();
                    text.text = dialogue_lines[index];
                }
            }
        }
    }

    public void StartTextBoxes(int line_index){
        if (panel != null){
            panel.SetActive(true);
        }

        if (freeze_time){
            Time.timeScale = 0.0f;
        }
        index = line_index;
        text.text = string.Empty;
        StopAllCoroutines();
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine(){
        foreach (char c in dialogue_lines[index].ToCharArray()){
            text.text += c;
            if (character_sound){
                if (c != ' '){
                    character_source.Play();
                }
            }
            yield return new WaitForSecondsRealtime(text_speed);
        }
    }

    void NextLine(){
        if (index < dialogue_lines.Length - 1){
            index = index + 1;
            text.text = string.Empty;
            StartCoroutine(TypeLine());
        }

        else{
            Time.timeScale = 1.0f;
            if (panel != null){
                panel.SetActive(false);
            }
            gameObject.SetActive(false);
            if (play_music){
                music_play.Play();
            }

            if (end){
                end_object.SetActive(true);
            }

            if(give_control){
                player.controllable = true;
            }
        }
    }

    void OnTriggerEnter(Collider collider){
        if (collider.CompareTag("Player")){
            Debug.Log("-_-");
            player = collider.GetComponent<NewMovement>();
            StartTextBoxes(0);
        }
    }
}
