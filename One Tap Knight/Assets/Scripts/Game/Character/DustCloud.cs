using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustCloud : MonoBehaviour {
	void Start () {
        Destroy(this.gameObject, 1f);
	}
}
