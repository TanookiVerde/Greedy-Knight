using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IScreen {
	void Prepare();
	void Close();
    IEnumerator BeginningAnimation();
}
