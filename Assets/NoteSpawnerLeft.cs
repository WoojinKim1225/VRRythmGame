using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteSpawnerLeft : MonoBehaviour
{
    public MidiManager midiManager;
    public GameObject notePrefab, subNotePrefab;

    private Vector3 noteOnPosition;
    private bool newPosition = false;
    
    private void OnEnable() {
        StartCoroutine(SpawnNote(midiManager.leftHandPosition));
        StartCoroutine(SpawnFingerNote(midiManager.leftHandFinger));
    }

    private IEnumerator SpawnNote(List<Note> notes) {
        int i = 0;
        while (i < notes.Count) {
            newPosition = true;
            GameObject note = Instantiate(notePrefab, transform.position, transform.rotation);
            NoteJudgement noteMover = note.GetComponent<NoteJudgement>();
            LineRenderer lineRenderer = note.GetComponent<LineRenderer>();
            lineRenderer.positionCount = 2;
            noteOnPosition = transform.position + (notes[i].pitch - 60) * 0.01f * Vector3.right;
            Vector3 noteOffPosition = noteOnPosition + (notes[i].endTime - notes[i].time) * 0.001f * noteMover.noteSpeed * Vector3.forward;
            lineRenderer.SetPosition(0, transform.InverseTransformPoint(noteOnPosition));
            lineRenderer.SetPosition(1, transform.InverseTransformPoint(noteOffPosition));
            yield return new WaitForSeconds((notes[i+1].time - notes[i].time)*0.001f);

            i++;
        }
        yield return null;
    }

    private IEnumerator SpawnFingerNote(List<Note> notes) {
        int j = 0;
        while (j < notes.Count) {
            GameObject subNote = Instantiate(subNotePrefab, noteOnPosition, transform.rotation);
            NoteJudgement noteJudgement = subNote.GetComponent<NoteJudgement>();
            if (newPosition) noteJudgement.pressButtonThenDownEnable = true;

            newPosition = false;

            if (notes[j].pitch == 21) {
                subNote.transform.Translate(-Vector3.right * 0.02f);
            } else if (notes[j].pitch == 22) {
            
            } else if (notes[j].pitch == 24) {
                subNote.transform.Translate(Vector3.right * 0.02f);
            }
            yield return new WaitForSeconds((notes[j+1].time - notes[j].time)*0.001f);

            j++;
        }
        yield return null;
    }
}
