using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public AudioMixer masterMixer;   // Reference to the AudioMixer
    public Slider volumeSlider;      // Reference to the UI Slider

    void Start()
    {
        // Set the initial value of the slider based on the current volume of the master group
        float currentVolume;
        masterMixer.GetFloat("MasterVolume", out currentVolume);
        volumeSlider.value = currentVolume;

        // Add a listener to the slider to handle value changes
        volumeSlider.onValueChanged.AddListener(SetMasterVolume);
    }

    // Method to set the master volume based on the slider's value
    public void SetMasterVolume(float sliderValue)
    {
        masterMixer.SetFloat("MasterVolume", sliderValue);
    }
}