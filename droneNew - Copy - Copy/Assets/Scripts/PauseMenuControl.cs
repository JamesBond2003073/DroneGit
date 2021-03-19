using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class PauseMenuControl : MonoBehaviour
{
    public GameObject btn_pause;
    public GameObject btn_play;
    public GameObject btn_back;
    public float pauseCheck=0;
    public float playCheck=0;
    public float backCheck=0;
    public float settingsCheck=0;
    public GameObject btn_restart;
    public GameObject btn_settings;
    public GameObject btn_quit;
    public GameObject sliderVolume;
    public GameObject slider2;
    public float timerPause = 0f;
    public float timerPlay = 0f;
    public float timerSettings = 0f;
    public float timerBack = 0f;
    public Animator animRestart;
    public Animator animSettings;
    public Animator animQuit;
    public Animator animVolume;
    public Camera main;
    public AudioMixer audioSetting;

    //Settings Save Parameters
public Slider Slider2;
public Slider volumeSlider;


    void Awake()
    {
        audioSetting.SetFloat("volumeMaster",PlayerPrefs.GetFloat("volume",0f));
    }
    void Start()
    {
        Time.timeScale = 1;
        volumeSlider.value = PlayerPrefs.GetFloat("volume",0f);	
    }

    
    public void ReturnMenu()
    {   Time.timeScale = 1;
        SceneManager.LoadScene("Menu");
    }

   
    public void SettingsMenu()
    {    playCheck = 0;
	     backCheck = 0;
		 pauseCheck = 0;
       
        
        settingsCheck = 1;
        Time.timeScale=0;
    }

   

    public void RestartGame()
    {
        SceneManager.LoadScene("lvl0wprototypeui");
    }

    public void VolumeControl(float volume)
	{
      audioSetting.SetFloat("volumeMaster",volume);
	  PlayerPrefs.SetFloat("volume",volume);
	}

    void Update()
    {   if(pauseCheck == 0 && settingsCheck == 0 && backCheck == 0)
		{if(Input.GetKeyDown(KeyCode.Escape))
		{
			 
       main.GetComponent<PostProcessingBehaviour>().enabled = true;
       Time.timeScale=0;
       pauseCheck = 1;
       playCheck = 0;
	   settingsCheck = 0;
	   backCheck = 0;
       
		}}
        //pause animations
       if(pauseCheck == 1)
		{  timerPlay = 0f;
		   timerBack = 0f;
		   timerSettings = 0f;
			
			timerPause += Time.unscaledDeltaTime;
      if(timerPause >= 0.0f)
		{
          btn_restart.SetActive(true);
		}
		if(timerPause >= 0.08f)
		{
			btn_settings.SetActive(true);
		}
		if(timerPause >= 0.16f)
		{
			btn_quit.SetActive(true);
			//pauseCheck = 0;
			
			if(Input.GetKeyDown(KeyCode.Escape))
		{ 
          playCheck = 1;
		  pauseCheck = 0;
		  settingsCheck = 0;
		  backCheck = 0;
		  timerPause = 0f;
		}
		}
		 
		}

        //play animations
        if(playCheck == 1)
		{  
			timerPlay += Time.unscaledDeltaTime;
			if(timerPlay >= 0.0f)
			{
			 animQuit.SetBool("play",true);
			}
			if(timerPlay >= 0.08f)
			{
			 animSettings.SetBool("play",true);
			}
			if(timerPlay >= 0.16f)
			{
			 animRestart.SetBool("play",true);
			}
			if(timerPlay >= 0.4f)
			{
				 animQuit.SetBool("play",false);
				 animSettings.SetBool("play",false);
				 animRestart.SetBool("play",false);
				 btn_restart.SetActive(false);
				 btn_settings.SetActive(false);
				 btn_quit.SetActive(false);
				main.GetComponent<PostProcessingBehaviour>().enabled = false;
                pauseCheck = 0;
				settingsCheck = 0;
				backCheck = 0;
			    timerPlay = 0f; 
				Time.timeScale = 1;
			}
            }

            //settings animations
            if(settingsCheck == 1)
			{   timerPause = 0f;
			    timerPlay = 0f;
				timerBack = 0f;
				 timerSettings += Time.unscaledDeltaTime;
				if(timerSettings >= 0.0f)
			{	 
				sliderVolume.SetActive(true);
			}
			if(timerSettings >= 0.08f)
			{	 
				//slider2.SetActive(true);
			}
				
				
			if(timerSettings >= 0.0f)
			{
			 animQuit.SetBool("play",true);
			}
			if(timerSettings >= 0.08f)
			{
			 animSettings.SetBool("play",true);
			}
			if(timerSettings >= 0.16f)
			{
			 animRestart.SetBool("play",true);
			}
			if(timerSettings >= 0.4f)
			{
				 animQuit.SetBool("play",false);
				 animSettings.SetBool("play",false);
				 animRestart.SetBool("play",false);
				 btn_restart.SetActive(false);
				 btn_settings.SetActive(false);
				 btn_quit.SetActive(false);
			
				
				//settingsCheck = 0f;
				 if(Input.GetKeyDown(KeyCode.Escape))
			{   
                pauseCheck = 0f;
				playCheck = 0f;
				settingsCheck = 0f;
				backCheck = 1f;
				timerSettings = 0f;
			}
			}	
		    }
            //back animations
            if(backCheck == 1)
			{      
                timerBack += Time.unscaledDeltaTime;
      if(timerBack >= 0.0f)
		{
          btn_restart.SetActive(true);
		 // anim2.SetBool("back",true);
		}
		if(timerBack >= 0.08f)
		{
			btn_settings.SetActive(true);
	     animVolume.SetBool("back",true);
		}
		if(timerBack >= 0.16f)
		{
			btn_quit.SetActive(true);
			
			
		}
		if(timerBack >= 0.4f)
		{   //slider2.SetActive(false);
		    sliderVolume.SetActive(false);
			//backCheck = 0f;
			
			if(Input.GetKeyDown(KeyCode.Escape))
			{   timerBack = 0f;
				playCheck = 1;
				pauseCheck = 0;
				backCheck = 0;
				settingsCheck = 0;
			}
		}
			}

       

    }
}
