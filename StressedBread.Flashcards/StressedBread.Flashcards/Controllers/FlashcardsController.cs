using StressedBread.Flashcards.Data;
using StressedBread.Flashcards.Data.Queries;
using StressedBread.Flashcards.DTOs;
using StressedBread.Flashcards.UI;

namespace StressedBread.Flashcards.Controllers;
internal class FlashcardsController
{
    private readonly FlashcardsUI _flashcardsUI;
    private readonly DatabaseAccess _databaseAccess;
    private readonly FlashcardsQueries _flashcardsQueries;

    private List<FlashcardsDTO> _flashcards = new();

    internal FlashcardsController(FlashcardsUI flashcardsUI, DatabaseAccess databaseAccess, FlashcardsQueries flashcardsQueries)
    {
        _flashcardsUI = flashcardsUI;
        _databaseAccess = databaseAccess;
        _flashcardsQueries = flashcardsQueries;
    }

    internal void ViewFlashcards(int stackId)
    {
        _flashcards = _databaseAccess.Reader<FlashcardsDTO>(_flashcardsQueries.GetFlashcardsQuery(), new { StackId = stackId });

        _flashcardsUI.FlashcardsStackView(_flashcards);
    }

    internal void AddFlashcard(int stackId)
    {
        var (question, answer) = _flashcardsUI.AddFlashcardView();
        _databaseAccess.ExecuteQuery(_flashcardsQueries.AddFlashcardQuery(), new { Question = question, Answer = answer, StackId = stackId });
    }

    internal void EditFlashcard(int stackId)
    {
        _flashcards = _databaseAccess.Reader<FlashcardsDTO>(_flashcardsQueries.GetFlashcardsQuery(), new { StackId = stackId });
        int flashcardIndex = _flashcardsUI.FlashcardsStackView(_flashcards);
        if (flashcardIndex > 0 && flashcardIndex <= _flashcards.Count)
        {
            var (question, answer) = _flashcardsUI.EditFlashcardView(_flashcards[flashcardIndex - 1]);

            if (String.Equals(question, "0")) question = _flashcards[flashcardIndex - 1].Question;
            if (String.Equals(answer, "0")) answer = _flashcards[flashcardIndex - 1].Answer;

            _databaseAccess.ExecuteQuery(_flashcardsQueries.EditFlashcardQuery(), new { Question = question, Answer = answer, _flashcards[flashcardIndex - 1].Id });
        }
    }

    internal void DeleteFlashcard(int stackId)
    {
        _flashcards = _databaseAccess.Reader<FlashcardsDTO>(_flashcardsQueries.GetFlashcardsQuery(), new { StackId = stackId });
        int flashcardIndex = _flashcardsUI.FlashcardsStackView(_flashcards);
        if (flashcardIndex > 0 && flashcardIndex <= _flashcards.Count)
        {
            _databaseAccess.ExecuteQuery(_flashcardsQueries.DeleteFlashcardQuery(), new { _flashcards[flashcardIndex - 1].Id });
        }
    }
}
