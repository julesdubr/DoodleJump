using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAppear : MonoBehaviour
{
    [SerializeField] AudioSource _appearSound;

    void OnBecameVisible() {
        _appearSound.Play();
    }
}
