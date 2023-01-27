using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObsShaker : MonoBehaviour {

    private Vector3 Amount = new Vector3(1f, 1f, 0);
    private float Duration = 1;
    public float Speed = 10;
    private AnimationCurve Curve = AnimationCurve.EaseInOut(0, 1, 1, 0);

    private bool DeltaMovement = true;

    private float time = 0;
    private Vector3 lastPos;
    private Vector3 nextPos;


    private void OnCollisionEnter(Collision collision) {
        Shake();
	}

    public void Shake() {
        Reset();
        time = Duration;
    }

    private void LateUpdate() {
        if (time > 0) {
            //do something
            time -= Time.deltaTime;
            if (time > 0) {
                //next position based on perlin noise
                nextPos = (Mathf.PerlinNoise(time * Speed, time * Speed * 2) - 0.5f) * Amount.x * transform.right * Curve.Evaluate(1f - time / Duration) +
                          (Mathf.PerlinNoise(time * Speed * 2, time * Speed) - 0.5f) * Amount.y * transform.up * Curve.Evaluate(1f - time / Duration);

                transform.Translate(DeltaMovement ? (nextPos - lastPos) : nextPos);

                lastPos = nextPos;
            }
        }
    }

    private void Reset() {
        //reset the last delta
        transform.Translate(DeltaMovement ? -lastPos : Vector3.zero);

        //clear values
        lastPos = nextPos = Vector3.zero;
    }
}

