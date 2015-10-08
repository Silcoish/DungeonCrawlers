/* Copyright (c) Dungeon Crawlers
*  Script Created by:
*  Corey Underdown
*/
 
using UnityEngine;
using UnityEngine.Audio; 

public class SetStartupMusic : MonoBehaviour
{
	public AudioMixerSnapshot startingSnapshot;

	void Start()
	{
		AudioManager.Inst.FadeMusic(startingSnapshot);
	}
}