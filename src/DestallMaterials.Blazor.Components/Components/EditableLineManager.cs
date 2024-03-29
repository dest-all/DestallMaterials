﻿namespace DestallMaterials.Blazor.Components;

internal class EditableLineManager<TLineModel>
{
    public EditableLineManager(TLineModel currentValue, int checkSum)
    {
        CurrentValue = currentValue;
        PreviousValue = currentValue;
        CheckSum = checkSum;
    }

    public TLineModel CurrentValue { get; set; }

    public TLineModel PreviousValue { get; set; }

    public Func<Task> Save { get; set; } = async () => { };

    public Func<Task> Delete { get; set; } = async () => { };

    public Action RevertChanges { get; set; } = async () => { };

    public Action Refresh { get; set; } = () => { };

    public int CheckSum { get; set; }

    public bool IsBeingEdited { get; set; }

    public bool NewlyAdded { get; set; }
}
