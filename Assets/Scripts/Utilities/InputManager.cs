// Created by Timo Heijne

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {

    /// <summary>
    /// Is any of the following key's pressed
    /// </summary>
    /// <param name="codes"></param>
    /// <returns></returns>
    public static bool IsAnyDown(KeyCode[] codes) {
        foreach (KeyCode code in codes) {
            if (Input.GetKey(code)) {
                return true;
            }
        }

        return false;
    }

    public static bool IsAllDown(KeyCode[] codes) {
        foreach (KeyCode code in codes) {
            if (!Input.GetKey(code)) {
                return false;
            }
        }

        return true;
    }
}
