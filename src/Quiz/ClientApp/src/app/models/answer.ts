export class Answer {
  id: number;
  text: string;
}

export class UserAnswer {
  answerText: string;
  isCorrect: boolean;
  answerCount: number;
}

export class UserResult {
  name: string;
  score: number;
}
