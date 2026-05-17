using StressedBread.Flashcards.Data;
using StressedBread.Flashcards.Data.Queries;
using StressedBread.Flashcards.DTOs;
using StressedBread.Flashcards.Models;
using StressedBread.Flashcards.UI;

namespace StressedBread.Flashcards.Controllers;
internal class StudyController
{
    private readonly DatabaseAccess _databaseAccess;
    private readonly StacksQueries _stacksQueries;
    private readonly FlashcardsQueries _flashcardsQueries;
    private readonly StudyQueries _studyQueries;
    private readonly StudyMenu _studyMenu;

    private List<StacksModel> _stacks = new();
    private StacksModel _currentStack = new();
    private List<FlashcardsDTO> _flashcards = new();

    private Random _random = new();

    private int _totalFlashcards = 0;
    private int _score = 0;

    internal StudyController(DatabaseAccess databaseAccess, StacksQueries stacksQueries, FlashcardsQueries flashcardsQueries, StudyQueries studyQueries, StudyMenu studyMenu)
    {
        _databaseAccess = databaseAccess;
        _stacksQueries = stacksQueries;
        _flashcardsQueries = flashcardsQueries;
        _studyQueries = studyQueries;
        _studyMenu = studyMenu;
    }

    internal void StudyStackSelection()
    {
        _stacks = _databaseAccess.Reader<StacksModel>(_stacksQueries.GetAllStacksQuery());
        _currentStack = _studyMenu.StudyMenuView(_stacks);
        _flashcards = _databaseAccess.Reader<FlashcardsDTO>(_flashcardsQueries.GetFlashcardsByStackIdQuery(), new { StackId = _currentStack.Id });
        _totalFlashcards = _flashcards.Count;

        if (_totalFlashcards == 0)
        {
            _studyMenu.NoFlashcardsInStackView();
            return;
        }

        StudyFlashcards();

        _databaseAccess.ExecuteQuery(_studyQueries.InsertStudySessionQuery(), new { Score = _score, SessionDate = DateTime.Now, StackId = _currentStack.Id });
        _score = 0;
    }

    internal void StudyFlashcards()
    {
        while (true)
        {
            FlashcardsDTO currentFlashcard = _flashcards[_random.Next(_flashcards.Count)];

            string answer = _studyMenu.StudyFlashcardView(currentFlashcard);

            if (String.Equals(answer.Trim(), "0", StringComparison.OrdinalIgnoreCase))
            {
                _studyMenu.StudyCompletedView(_score, _totalFlashcards);
                return;
            }

            bool isCorrect = String.Equals(answer.Trim(), currentFlashcard.Answer.Trim(), StringComparison.OrdinalIgnoreCase);

            if (isCorrect) _score++;

            _studyMenu.IsCorrectAnswerView(isCorrect, currentFlashcard);

            _flashcards.Remove(currentFlashcard);

            if (_flashcards.Count == 0)
            {
                _studyMenu.StudyCompletedView(_score, _totalFlashcards);
                return;
            }
        }
    }

    internal void ViewStudySessions()
    {
        _stacks = _databaseAccess.Reader<StacksModel>(_stacksQueries.GetAllStacksQuery());
        string result = _studyMenu.GetStackNameInput(_stacks);

        if (String.Equals(result.Trim(), "0", StringComparison.OrdinalIgnoreCase))
            return;
        else if (String.Equals(result.Trim(), "1", StringComparison.OrdinalIgnoreCase))
        {
            List<StudySessionsDTO> studySessions = _databaseAccess.Reader<StudySessionsDTO>(_studyQueries.GetStudySessionsQuery(), new { StackId = (int?)null });
            _studyMenu.ViewStudySessions(studySessions);
        }
        else
        {
            StacksModel stack = _stacks.First(s => s.Name.Equals(result, StringComparison.OrdinalIgnoreCase));

            List<StudySessionsDTO> studySessions = _databaseAccess.Reader<StudySessionsDTO>(_studyQueries.GetStudySessionsQuery(), new { StackId = stack.Id });
            _studyMenu.ViewStudySessions(studySessions);
        }
    }
}
