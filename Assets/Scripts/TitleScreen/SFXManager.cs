using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour {
    [SerializeField] private AudioSource _openSFX;
    [SerializeField] private AudioSource _closeSFX;
    [SerializeField] private AudioSource _chooseSFX;
    [SerializeField] private AudioSource _missSFX;
    [SerializeField] private AudioSource _hitSFX;
    [SerializeField] private AudioSource _successSFX;
    [SerializeField] private AudioSource _failSFX;
    
    public void PlayOpen() {
        _openSFX.Play();
    }
    
    public void PlayClose() {
        _closeSFX.Play();
    }
    
    public void PlayChoose() {
        _chooseSFX.Play();
    }
    
    public void PlayMiss() {
        _missSFX.time = 0.1f;
        _missSFX.Play();
    }
    
    public void PlayHit() {
        _hitSFX.time = 0.1f;
        _hitSFX.Play();
    }
    
    public void PlaySuccess() {
        _successSFX.Play();
    }
    
    public void PlayFail() {
        _failSFX.Play();
    }
}