using Melanchall.DryWetMidi.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using Unity.VisualScripting;
using Melanchall.DryWetMidi.Interaction;
using System.Linq;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using Melanchall.DryWetMidi.MusicTheory;


// 미디 파일 내 노트 정보를 저장하는 클래스
[System.Serializable]
public class Note
{
    [Tooltip("노트 시작 시간 (밀리세컨드)")]
    public long time;
    [Tooltip("노트 종료 시간 (밀리세컨드)")]
    public long endTime;
    [Tooltip("음 높이 (C4 = 60 기준, 0에서 127 사이의 값)")]
    public int pitch;
    public NoteName pitchName;
    [Tooltip("음의 세기 (0에서 127 사이의 값)")]
    public int velocity;

    public Note(long time, long endTime, int pitch, NoteName pitchName, int velocity) {
        this.time = time;
        this.endTime = endTime;
        this.pitch = pitch;
        this.pitchName = pitchName;
        this.velocity = velocity;
    }
}

[System.Serializable]
public class TempoData
{
    public long StartTime;
    public double QuaterNotesPerMillisecond;

    public TempoData(long startTime, double quaterNotesPerMillisecond)
    {
        StartTime = startTime;
        QuaterNotesPerMillisecond = quaterNotesPerMillisecond;
    }
}


public class MidiManager : MonoBehaviour
{
    public string fileDirectory = ""; // 미디 파일 경로
    private MidiFile midi;
    private Melanchall.DryWetMidi.Interaction.Note[] array;
    private MidiManager midiManager;

    public List<TempoData> tempoDataList;

    public List<Note> leftHandFinger, leftHandPosition, leftHandRender;
    public List<Note> rightHandFinger, rightHandPosition, rightHandRender;

    private void Start() {
        ImportMidi();
        ExtractTempoData();
    } 

    private void ImportMidi()
    {
        midiManager = FindObjectOfType<MidiManager>();
        try
        {
            midi = MidiFile.Read(Application.dataPath + "/" + fileDirectory);
            GetDataFromMidi(midi);

            midiManager.leftHandFinger.Clear();
            midiManager.leftHandPosition.Clear();
            midiManager.leftHandRender.Clear();
            midiManager.rightHandFinger.Clear();
            midiManager.rightHandPosition.Clear();
            midiManager.rightHandRender.Clear();
            foreach (var note in array)
            {
                if (note.Channel == 1) {
                    if (note.NoteNumber <= 24)
                        leftHandFinger.Add(new Note(note.Time, note.EndTime, note.NoteNumber, note.NoteName, note.Velocity));
                    else
                        leftHandPosition.Add(new Note(note.Time, note.EndTime, note.NoteNumber, note.NoteName, note.Velocity));
                } else {
                    if (note.NoteNumber <= 24)
                        rightHandFinger.Add(new Note(note.Time, note.EndTime, note.NoteNumber, note.NoteName, note.Velocity));
                    else
                        rightHandPosition.Add(new Note(note.Time, note.EndTime, note.NoteNumber, note.NoteName, note.Velocity));
                }
                
            }
            Debug.Log("Import Successful");
        }
        catch (IOException e)
        {
            Debug.LogError("Error reading MIDI file: " + e.Message);
        }
    }

    private void GetDataFromMidi(MidiFile midi)
    {
        var midiNotes = midi.GetNotes();
        array = new Melanchall.DryWetMidi.Interaction.Note[midiNotes.Count];
        midiNotes.CopyTo(array, 0);
    }

    private void ExtractTempoData() {
        
    }
}