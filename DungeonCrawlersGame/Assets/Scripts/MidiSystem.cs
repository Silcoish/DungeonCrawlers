using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

/*************************************************
 * MIDI to TEXT converter
 * http://flashmusicgames.com/midi/mid2txt.php
 * Thankyou internet
**************************************************/

public class MidiSystem : MonoBehaviour {

	[System.Serializable]
	public class MidiNote
	{
		public enum State
		{
			ON,
			OFF
		};

		[SerializeField] public int millisecond;
		[SerializeField] public State state;
		[SerializeField] public int channel;
		[SerializeField] public int note;
		[SerializeField] public int velocity;

		public MidiNote(string[] pram)
		{
			millisecond = int.Parse(pram[0]);
			if (pram[1].Contains("On")) state = State.ON;
			else state = State.OFF;
			channel = int.Parse(pram[2]);
			note = int.Parse(pram[3]);
			velocity = int.Parse(pram[4]);
		}
	};

	[SerializeField] string filePath;
	[SerializeField] List<MidiNote> midiNotes;
	List<NoteSubscribe> subscribers;

	private float counter = 0.0f;
	private int index = 0;

	void Awake()
	{
		midiNotes = new List<MidiNote>();
		subscribers = new List<NoteSubscribe>();
	}

	void Start () {
		ReadFile();
	}

	void Update()
	{	
		counter += Time.deltaTime * 1000;

		while(counter >= midiNotes[index].millisecond)
		{
			for (int i = 0; i < subscribers.Count; i++)
			{
				if(midiNotes[index].note == subscribers[i].note)
				{
					if(midiNotes[index].velocity >= subscribers[i].note)
					{
						//if (midiNotes[index].state == MidiNote.State.ON)
							subscribers[i].Activate();
					}
					else
					{
							subscribers[i].Deactivate();
					}
				}
			}
			index++;
			if(index >= midiNotes.Count)
			{
				counter = counter - midiNotes[index - 1].millisecond;
				index = 0;
				break;
			}
		}
	}

	private void ReadFile()
	{
		StreamReader sr = new StreamReader(Application.dataPath + "/Audio/" + filePath + ".miditext");
		string fileContents = sr.ReadToEnd();
		sr.Close();

		var lines = fileContents.Split("\n"[0]);
		foreach(string line in lines)
		{
			//milliseconds
			//on/off
			//channel
			//note
			//velocity

			string[] splitString = line.Split(" "[0]);
			splitString[2] = splitString[2].Substring(3);
			splitString[3] = splitString[3].Substring(2);
			splitString[4] = splitString[4].Substring(2);
			MidiNote tempM = new MidiNote(splitString);

			midiNotes.Add(tempM);

		}
	}

	public void Subscribe(NoteSubscribe sub)
	{
		subscribers.Add(sub);
	}
}
