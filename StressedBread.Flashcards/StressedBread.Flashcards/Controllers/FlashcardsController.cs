using StressedBread.Flashcards.Data;
using StressedBread.Flashcards.Data.Queries;
using StressedBread.Flashcards.DTOs;
using StressedBread.Flashcards.UI;
using static StressedBread.Flashcards.Enums;

namespace StressedBread.Flashcards.Controllers;
internal class FlashcardsController
{
    private readonly FlashcardsUI _flashcardsUI;
    private readonly DatabaseAccess _databaseAccess;
    private readonly FlashcardsQueries _flashcardsQueries;

    private List<FlashcardsDTO> _flashcards = new();
    private List<AllFlashcardsDTO> _allFlashcards = new();

    internal FlashcardsController(FlashcardsUI flashcardsUI, DatabaseAccess databaseAccess, FlashcardsQueries flashcardsQueries)
    {
        _flashcardsUI = flashcardsUI;
        _databaseAccess = databaseAccess;
        _flashcardsQueries = flashcardsQueries;
    }

    internal List<FlashcardsDTO> GetFlashcardsByStackId(int stackId)
    {
        return _databaseAccess.Reader<FlashcardsDTO>(_flashcardsQueries.GetFlashcardsByStackIdQuery(), new { StackId = stackId });
    }

    internal FlashcardsDTO? GetFlashcardById(int flashcardId)
    {
        return _databaseAccess.Reader<FlashcardsDTO>(_flashcardsQueries.GetFlashcardByIdQuery(), new { Id = flashcardId }).FirstOrDefault();
    }

    internal void ViewFlashcards(int stackId)
    {
        _flashcards = GetFlashcardsByStackId(stackId);
        _flashcardsUI.ViewFlashcards(_flashcards);
    }

    internal void AddFlashcard(int stackId)
    {
        var (question, answer) = _flashcardsUI.AddFlashcardView();
        _databaseAccess.ExecuteQuery(_flashcardsQueries.AddFlashcardQuery(), new { Question = question, Answer = answer, StackId = stackId });
    }

    internal void EditFlashcard(int stackId)
    {
        _flashcards = GetFlashcardsByStackId(stackId);
        int selectedDisplayId = _flashcardsUI.FlashcardsStackView(_flashcards);

        if (selectedDisplayId == 0)
            return;

        int selectedRealId = selectedDisplayId > 0 && selectedDisplayId <= _flashcards.Count ? _flashcards[selectedDisplayId - 1].Id : -1;

        EditFlashcardById(selectedRealId);
    }

    internal void EditFlashcardById(int flashcardId)
    {
        var currentFlashcard = GetFlashcardById(flashcardId);

        if (flashcardId == -1 || currentFlashcard == null)
        {
            _flashcardsUI.InvalidFlashcardIdMessage();
            return;
        }
        
        var (question, answer) = _flashcardsUI.EditFlashcardView(currentFlashcard);

        if (String.Equals(question, "0")) question = currentFlashcard.Question;
        if (String.Equals(answer, "0")) answer = currentFlashcard.Answer;

        _databaseAccess.ExecuteQuery(_flashcardsQueries.EditFlashcardQuery(), new { Id = flashcardId, Question = question, Answer = answer});
    }

    internal void DeleteFlashcard(int stackId)
    {
        _flashcards = GetFlashcardsByStackId(stackId);
        int selectedDisplayId = _flashcardsUI.FlashcardsStackView(_flashcards);
         
        if (selectedDisplayId == 0)
            return;

        int selectedRealId = selectedDisplayId > 0 && selectedDisplayId <= _flashcards.Count ? _flashcards[selectedDisplayId - 1].Id : -1;

        DeleteFlashcardById(selectedRealId);
    }

    internal void DeleteFlashcardById(int flashcardId)
    {
        if (flashcardId == -1)
        {
            _flashcardsUI.InvalidFlashcardIdMessage();
            return;
        }

        _databaseAccess.ExecuteQuery(_flashcardsQueries.DeleteFlashcardQuery(), new { Id = flashcardId });
    }

    internal void FlashcardsMenu()
    {
        while (true)
        {
            _allFlashcards = _databaseAccess.Reader<AllFlashcardsDTO>(_flashcardsQueries.GetAllFlashcardsQuery());
            int selectedFlashcardId = _flashcardsUI.ViewAllFlashcards(_allFlashcards);

            if (selectedFlashcardId == 0)
                return;

            var selectedFlashcard = _allFlashcards.First(f => f.Id == selectedFlashcardId);

            var option = _flashcardsUI.ManageFlashcardMenuView(selectedFlashcard.Question);

            switch (option)
            {
                case FlashcardMenuOption.EditFlashcard:
                    EditFlashcardById(selectedFlashcardId);
                    break;
                case FlashcardMenuOption.DeleteFlashcard:
                    DeleteFlashcardById(selectedFlashcardId);
                    break;
                case FlashcardMenuOption.BackToMainMenu:
                    return;
            }
        }
    }
}
