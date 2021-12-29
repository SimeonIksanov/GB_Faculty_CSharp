'use strict'
class Question{
    constructor(question, answers, rightAnswer){
        this.text = question;
        this.answers = answers;
        this.rightAnswer = rightAnswer;
    }
}

let questionDB = {
    questions : [
        new Question("Сколько лапок у мухи?", [2,4,6,8], 3),
        new Question("Сколько песчинок в куче песка?", [1,2,'много','очень много'], 4),
        new Question("В каких фруктах нашли Чебурашку?", ['апельсины','яблоки','груши','арбузы'], 1),
        new Question("Two Zero Two Zero", ['2024','0024','0044','2044'], 4),
        new Question("При повороте автомобиля влево, какой колесо не крутится", ['заднее левое','рулевое','запасное','все'], 3),
        new Question("2+2 = ?", [2,4,6,5], 2),
    ],

    getQuestion(){
        let rnd = Math.floor(Math.random() * this.questions.length);
        return this.questions[rnd];
    }
}