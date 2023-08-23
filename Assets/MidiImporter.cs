using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Melanchall.DryWetMidi.Interaction;
using Melanchall.DryWetMidi.Core;
using System.IO;

[System.Serializable]
public class Note {
    public long startTime;
    public long endTime;
    public byte pitch;
    public byte velocity;

    public long length;

    public Note(long startTime, long endTime, byte pitch, byte velocity) {
        this.startTime = startTime;
        this.endTime = endTime;
        this.pitch = pitch;
        this.velocity = velocity;
        length = endTime - startTime;
    }
}

[System.Serializable]
public class Tempo {
    public long changeTime;
    public long quaterNotesPerMillisecond;

    public Tempo(long changeTime, long quaterNotesPerMillisecond) {
        this.changeTime = changeTime;
        this.quaterNotesPerMillisecond = quaterNotesPerMillisecond;
    }
}

[System.Serializable]
public class TimeSig {
    public long changeTime;
    public int numerator;
    public int denominator;
    
    public TimeSig(long changeTime, int numerator, int denominator) {
        this.changeTime = changeTime;
        this.numerator = numerator;
        this.denominator = denominator;
    }
}

public class MidiImporter : MonoBehaviour
{
    public string fileDirectory = "";
    private MidiFile midi;
    private Melanchall.DryWetMidi.Interaction.Note[] array;

    public List<Note> finger_L, position_L, articulation_L;
    public List<Note> finger_R, position_R, articulation_R;

    public List<Tempo> tempi;

    public List<TimeSig> timeSigs;

    private void Start() {
        ImportMidi();
    }

    private void ImportMidi() {
        try {
            midi = MidiFile.Read(Application.dataPath + "/" + fileDirectory);

            var midiNotes = midi.GetNotes();
            var midiTempi = midi.GetTempoMap();

            array = new Melanchall.DryWetMidi.Interaction.Note[midiNotes.Count];
            midiNotes.CopyTo(array, 0);

            var tempoChanges = midiTempi.GetTempoChanges();
            var timeSigChanges = midiTempi.GetTimeSignatureChanges();

            tempi.Clear();
            
            foreach (var tempoChange in tempoChanges) {
                var t = tempoChange.Time;
                var q = midiTempi.GetTempoAtTime((MidiTimeSpan)t).MicrosecondsPerQuarterNote;
                tempi.Add(new Tempo(t, q));
            } 
            if (tempi.Count == 0) {
                tempi.Add(new Tempo(0, 500000));
            } else if (tempi[0].changeTime != 0) {
                tempi.Insert(0, new Tempo(0, 500000));
            }

            timeSigs.Clear();
            foreach (var timeSigChange in timeSigChanges) {
                var t = timeSigChange.Time;
                var n = timeSigChange.Value.Numerator;
                var d = timeSigChange.Value.Denominator;
                timeSigs.Add(new TimeSig(t, n, d));
            }
            if (timeSigs.Count == 0) {
                timeSigs.Add(new TimeSig(0, 4, 4));
            } else if (timeSigs[0].changeTime != 0) {
                timeSigs.Insert(0, new TimeSig(0, 4, 4));
            }

            finger_L.Clear();
            position_L.Clear();
            articulation_L.Clear();
            finger_R.Clear();
            position_R.Clear();
            articulation_R.Clear();
            foreach(var note in array) {
                if (note.Channel == 1) {
                    if (note.NoteNumber == 21) finger_L.Add(new Note(note.Time, note.EndTime, 4, note.Velocity));
                    else if (note.NoteNumber == 23) finger_L.Add(new Note(note.Time, note.EndTime, 2, note.Velocity));
                    else if (note.NoteNumber == 24) finger_L.Add(new Note(note.Time, note.EndTime, 1, note.Velocity));
                    else if (note.NoteNumber > 96) articulation_L.Add(new Note(note.Time, note.EndTime, (byte)(note.NoteNumber - 96), note.Velocity));
                    else position_L.Add(new Note(note.Time, note.EndTime, note.NoteNumber, note.Velocity));
                } else {
                    if (note.NoteNumber == 21) finger_R.Add(new Note(note.Time, note.EndTime, 1, note.Velocity));
                    else if (note.NoteNumber == 23) finger_R.Add(new Note(note.Time, note.EndTime, 2, note.Velocity));
                    else if (note.NoteNumber == 24) finger_R.Add(new Note(note.Time, note.EndTime, 4, note.Velocity));
                    else if (note.NoteNumber > 96) articulation_R.Add(new Note(note.Time, note.EndTime, (byte)(note.NoteNumber - 96), note.Velocity));
                    else position_R.Add(new Note(note.Time, note.EndTime, note.NoteNumber, note.Velocity));
                }
            }

        } catch (IOException e) {
            Debug.LogError("Error reading MIDI file: " + e.Message);
        }
    }
}
