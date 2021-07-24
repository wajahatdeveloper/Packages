using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.Serialization;
using Lean.Pool;
using System.Linq;
using Sirenix.OdinInspector;
using AsCoroutine;
using UnityEngine.Audio;

[System.Serializable]
public class AudioItem
{
	public string name;
	public AudioClip clip;
}

public class AudioController :SingletonBehaviour<AudioController>
{
	[ShowInInspector]private GameObject soundObject;
	[ShowInInspector]private GameObject musicObject;
	[ShowInInspector]private AudioMixer soundMixer;
	[ShowInInspector]private AudioMixer musicMixer;
	[ShowInInspector]private List<AudioItem> sounds = new List<AudioItem>();
	[ShowInInspector]private List<AudioItem> musics = new List<AudioItem>();
	private LeanGameObjectPool soundPool;

	private void Start()
	{
		soundPool = GetComponent<LeanGameObjectPool>();

		//StartCoroutine( Test() );
	}

	//public IEnumerator Test()
	//{
	//	AudioController.instance.PlayMusic( "1" );
	//	while (true)
	//	{
	//		yield return new WaitForSeconds( 0.2f );
	//		AudioController.instance.PlaySound( "1" );
	//	}
	//}

	private void PlayClip( AudioClip clip, GameObject audioItemObject , bool isMusic = false )
	{
		var audioItemSrc = audioItemObject.GetComponent<AudioSource>();
		audioItemSrc.clip = clip;
		audioItemSrc.Play();
		if (isMusic)
		{
			LeanPool.Despawn( audioItemSrc.gameObject, clip.length );
		}
		else
		{
			soundPool.Despawn( audioItemSrc.gameObject, clip.length );
		}
	}

	public void PlayMusic( string name )
	{
		PlayClip( musics.Single( x => x.name == name ).clip, LeanPool.Spawn(musicObject,transform) , true );
	}

	public void PlaySound( string name )
	{
		PlayClip( sounds.Single( x => x.name == name ).clip, soundPool.Spawn( transform ) );
	}

	public void PlaySound( AudioClip clip )
	{
		PlayClip( clip , soundPool.Spawn( transform ) );
	}

	public void PlaySoundAtPosition( Vector3 position , AudioClip clip)
	{
		var audioItemObject = soundPool.Spawn( position, Quaternion.identity, transform );
		PlayClip( clip, audioItemObject );
	}

	public void PlaySoundAtPosition( Vector3 position, string name )
	{
		var audioItemObject = soundPool.Spawn( position, Quaternion.identity, transform );
		PlayClip( sounds.Single( x => x.name == name ).clip, audioItemObject );
	}

	public void AddSound(string name, AudioClip clip)
	{
		sounds.Add( new AudioItem() { name = name, clip = clip } );
	}

	public float GlobalVolume { get => AudioListener.volume; set => AudioListener.volume = value; }
	public float SoundVolume { get { float volume = 0f; soundMixer.GetFloat( "SoundVolume", out volume ); return volume; } set => soundMixer.SetFloat( "SoundVolume", value ); }
	public float MusicVolume { get { float volume = 0f; musicMixer.GetFloat( "MusicVolume", out volume ); return volume; } set => musicMixer.SetFloat( "MusicVolume", value ); }
}