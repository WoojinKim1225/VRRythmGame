using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleNoteKiller : MonoBehaviour
{
    // 충돌한 물체가 트리거 영역 안으로 들어왔을 때 호출되는 메서드
    private void OnTriggerEnter(Collider other) {
        // 충돌한 물체의 게임 오브젝트 파괴
        Destroy(other.gameObject);
    }
}