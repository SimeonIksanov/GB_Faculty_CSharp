'use strict'
let renderer = {
    printQuestion(question){
        console.clear();
        console.log(`Вопрос: ${question.text}`);
        console.log("Варианты ответов:");
        for (let i = 1; i <= question.answers.length; i++) {
            console.log(`${i}) ${question.answers[i-1]}`);
        }
    },

    welcomeMessage() {
        console.clear();
        console.log("Это игра кто хочет стать миллионером!\n\n");
    },

    showScore(player) {
        console.clear();
        console.log(`Игрок набрал ${player.score} очков`);
    }
}