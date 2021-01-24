Test task: Quote quiz 

Create a new MVC Core API application.
Use EF core code first to define a schema for a database holding Authors, Quotes (from the author) and Category(for a quote). One quote can have one Author and one category.
Create an import Controller with one method that would Import a quote with an author and a category from a CSV file.
Create a Quiz Controller with a method that would build a quiz from the database with 10 questions and 3 possible answers for each question. (The idea is for the method to return enough data for a FE to display the quiz)

Add an additional parameter: QuizTheme to the quiz returned from the QuizController
Make an API call to the following method to check the weather:
api.openweathermap.org/data/2.5/weather?q={city name}&appid={API key}
	Documentation can be found here https://openweathermap.org/current
	Use the following API key: 59d5dea723b79febd161eb8634538449
	If the weather is Clear return a LightTheme for QuizTheme, if it is something else return DarkTheme.


Bonus: Use repository pattern.
Bonus: Set up swagger for easier testing.
Bonus Seed some data: example https://drive.google.com/file/d/1MlgQPCoGK69cJrn6Nf26Wguo5QNzecZw/view?usp=sharing
