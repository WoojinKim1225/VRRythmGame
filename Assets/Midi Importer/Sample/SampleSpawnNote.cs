using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class SampleSpawnNote : MonoBehaviour
{
    public GameObject notePrefab; // 생성할 노트 게임 오브젝트 프리팹

    // 미디 음 높이(pitch)에 따라 노트 생성
    public void spanwNote(int pitch) {
        // MIDI 음 높이에 따라 노트 오브젝트 생성
        GameObject note = Instantiate(notePrefab, (pitch - 60) * Vector3.right, quaternion.identity);

        // 생성된 노트 오브젝트를 위로 움직이는 Coroutine 시작
        StartCoroutine(noteMove(note));
    }

    // 노트 오브젝트를 위로 움직이는 Coroutine
    public IEnumerator noteMove(GameObject note) {
        while (note != null)
        {
            // 노트 오브젝트의 Rigidbody를 이용하여 위로 이동
            note.GetComponent<Rigidbody>().MovePosition(note.transform.position + Vector3.up * Time.fixedDeltaTime * 5f);

            // 다음 FixedUpdate까지 기다림
            yield return new WaitForFixedUpdate();
        }
        yield return null;
    }
}
