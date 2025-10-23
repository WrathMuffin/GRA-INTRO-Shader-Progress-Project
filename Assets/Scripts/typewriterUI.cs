// Script for having a typewriter effect for UI
// Prepared by Nick Hwang (https://www.youtube.com/nickhwang)
// Want to get creative? Try a Unicode leading character(https://unicode-table.com/en/blocks/block-elements/)
// Copy Paste from page into Inpector

using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using Unity.VisualScripting;

public class typewriterUI : MonoBehaviour
{
	public TMP_Text _tmpProText;
	public List<string> writer = new List<string>();
    public bool done_current = false;
    public int index = 0;
	public AudioSource voice_source;
	public AudioSource box_source;
	public List<AudioClip> voice_clips = new List<AudioClip>();
	int last_index = 0;
	int voice_index = 0;
	public GameObject arrow;
	public bool cutscene = false;

	[SerializeField] float delayBeforeStart = 0f;
	[SerializeField] float timeBtwChars = 0.1f;
	[SerializeField] string leadingChar = "";
	[SerializeField] bool leadingCharBeforeDelay = false;

	// Use this for initialization
	void Start()
	{
		arrow.SetActive(false);

		if (_tmpProText != null)
		{
			_tmpProText.text = "";

			StartCoroutine("TypeWriterTMP");
		}
	}

    void Update()
    {
		if (cutscene){
			if (Input.GetKeyDown(KeyCode.Return)){
				index = index + 1;
				arrow.SetActive(false);
				//box_source.Play();
				StartCoroutine("TypeWriterTMP");
			}
		}
    }

	IEnumerator TypeWriterTMP()
    {
        _tmpProText.text = leadingCharBeforeDelay ? leadingChar : "";

        yield return new WaitForSeconds(delayBeforeStart);

		foreach (char c in writer[index])
		{
			if (_tmpProText.text.Length > 0)
			{
				while(voice_index == last_index){
					voice_index = Random.Range(0, voice_clips.Count);
				}
				
				last_index = voice_index;

				// voice_source.clip = voice_clips[Random.Range(0, voice_clips.Count)];
				// if (c != ' '){

				//voice_source.PlayOneShot(voice_clips[voice_index]);

				//}
				_tmpProText.text = _tmpProText.text.Substring(0, _tmpProText.text.Length - leadingChar.Length);
			}

			_tmpProText.text += c;
			_tmpProText.text += leadingChar;
			yield return new WaitForSeconds(timeBtwChars);
		}
		
		arrow.SetActive(true);

		if (leadingChar != "")
		{
			_tmpProText.text = _tmpProText.text.Substring(0, _tmpProText.text.Length - leadingChar.Length);
		}
	}

	public void StartText(string simple_text){
		writer.Clear();
		writer.Add(simple_text);
		StartCoroutine("TypeWriterTMP");
	}

	void OnTriggerEnter(Collider collider){
		if(collider.CompareTag("Player")){
			StartCoroutine("TypeWriterTMP");
		}
	}
}