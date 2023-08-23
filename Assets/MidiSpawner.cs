using System;
using System.Collections;
using System.Collections.Generic;
using Melanchall.DryWetMidi.MusicTheory;
using Unity.Mathematics;
using UnityEngine;

public class MidiSpawner : MonoBehaviour
{
    public MidiImporter midiImporter;
    public GameObject notePrefab;
    public GameObject subNote_Prefab_primary;
    public GameObject subNote_Prefab_trigger;
    public GameObject subNote_Prefab_grip;

    public float distancePerSemiNote;
    public float distancePerMilliSeconds;

    private float qpms;

    public float minimumHoldNoteLength;
    Vector3 noteOnPosition;
    public enum Articulation {Normal, Legato};
    public Articulation articulation;

    public bool newPosition = false;

    private void OnEnable() {
        StartCoroutine(ChangeTempo(midiImporter.tempi));
        StartCoroutine(SpawnNote(midiImporter.position_R));
    }

    private IEnumerator ChangeTempo(List<Tempo> l) {
        for (int i = 0; i < l.Count; i++) {
            qpms = l[i].quaterNotesPerMillisecond;
            yield return new WaitForSeconds(l[i+1].changeTime - l[i].changeTime);
        }
        yield return null;
    }

    private IEnumerator SpawnNote(List<Note> l) {
        for (int i = 0; i < l.Count; i++) {
            newPosition = true;
            GameObject note = Instantiate(notePrefab, transform.position, transform.rotation);
            note.name = l[i].pitch.ToString();
            LineRenderer line = note.GetComponentInChildren<LineRenderer>();

            noteOnPosition = transform.position + (l[i].pitch - 60) * distancePerSemiNote * Vector3.right;
            line.SetPosition(0, transform.InverseTransformPoint(noteOnPosition));

            Vector3 noteOffPosition = noteOnPosition + (l[i].endTime - l[i].startTime) * distancePerMilliSeconds * 500000f / qpms * Vector3.up;
            line.SetPosition(1, transform.InverseTransformPoint(noteOffPosition));

            note.GetComponent<NoteMechanism>().OnStart(l[i].endTime - l[i].startTime >= minimumHoldNoteLength, distancePerMilliSeconds, qpms);

            Debug.Log(i);   
            if (i+1 == l.Count) yield break;
            yield return new WaitForSeconds((l[i+1].startTime - l[i].startTime) * distancePerMilliSeconds);
        }
        yield return null;
    }

    private IEnumerator SpawnSubNote(List<Note> l) {
        for (int i = 0; i < l.Count; i++) {
            GameObject note;
            if (l[i].pitch == 1) note = Instantiate(subNote_Prefab_primary, noteOnPosition, transform.rotation);
            if (l[i].pitch == 2) note = Instantiate(subNote_Prefab_trigger, noteOnPosition, transform.rotation);
            if (l[i].pitch == 4) note = Instantiate(subNote_Prefab_grip, noteOnPosition, transform.rotation);
        }
        yield return null;
    }
}
