using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerDefeat : StateMachineBehaviour
{
    private SoundManager m_SoundManager;
    
    
    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
        GameObject.FindGameObjectsWithTag("EnemyManager")[0].GetComponent<CoroutineHandler>().createCoroutine(waitForDuration(animatorStateInfo.length * 2));
    }

    IEnumerator waitForDuration(float duration) {
        yield return new WaitForSeconds(duration);
        activateDefeatMenu();
    }

    private void activateDefeatMenu() {
        GameObject[] list = Resources.FindObjectsOfTypeAll(typeof(GameObject)) as GameObject[];
        m_SoundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        for (int i = 0; i < list.Length; i++) {
            if(list[i].CompareTag("DefeatMenu")) {
                Time.timeScale = 0f;
                m_SoundManager.StopAllSounds();
                list[i].SetActive(true);
                break;
            }
        }
    }
}
