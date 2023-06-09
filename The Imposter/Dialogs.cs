﻿using System.Collections.Generic;

using TheImposter.GameStates;

namespace TheImposter;
internal static class Dialogs
{
    private static readonly Dictionary<int, string> messages = new()
    {
        { 1, "An evil skeleton lurks among your people - can you uncover its identity before time runs out?" },
        { 2, "The evil skeleton has called for reinforcements - now there are two! Beware, for their numbers will increase with every even round." },
        { 3, "Beware, the evil skeletons have learned to disguise themselves by wearing clothes. Can you spot the bones beneath the garments, or will they deceive you?" },
        { 5, "The evil skeletons have gained a new power - they can now transform into humans. However, this ability comes with a cost - they can no longer wear clothes. Will this make them easier to identify, or will their human form make them even more elusive?" },
        { 7, "Cunning as ever, the skeletons have found a new way to deceive. They can now dress themselves in human clothing, but in doing so, they have lost their ability to move. Keep your eyes peeled for any suspiciously still figures, and don't let their disguise fool you. Remember, most regular people don't stand in one place for more than 5 seconds." },
        { 9, "The skeletons in disguise have stepped up their game. They have regained their ability to move, but something seems off about their movements - they have lost their animations. Can you spot the uncanny movements of the disguised skeletons, and put an end to their devious plans before it's too late?" },
        { 11, "The skeletons have become even more adept at blending in. They are now fully clothed and move with realistic animations, but they still have a telltale sign - a bloodcurdling shriek when approached. Keep your ears open for any suspicious sounds, and be ready to act quickly when you hear them. Can you outsmart the skeletons in disguise and emerge victorious?" },
    };

    public static PauseDialogState GetDialog(int stage)
    {
        if (messages.TryGetValue(stage, out string value))
            return new PauseDialogState(value);
        
        return null;
    }
}
