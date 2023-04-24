# Data analytics - basics of Jupyter
This GitHub repository contains a collection of tips and tricks for working with Jupyter.  It demonstrates some of the basic techniques analysts use for capturing and storing data in various databases.  Here's a brief description of the tutorials available in this repository.

## Tutorials

* [Basics of Jupyter](https://github.com/tspence/data-analysis-class-resources/blob/main/jupyter/Basics%20of%20Jupyter.ipynb) - A notebook that demonstrates how you can verify if Jupyter and Python have been installed correctly on your computer.  Also includes a demonstration of how many Jupyter implementations will allow you to install Python packages directly using code blocks.
* [Basics of Loading Data](https://github.com/tspence/data-analysis-class-resources/blob/main/jupyter/Basics%20of%20Loading%20Data.ipynb) - Demonstrates how to read files from disk and to examine them with Pandas.  Includes examples for connecting to a MongoDB server, storing your connection string in a file outside of your github repository, and some techniques for querying data in MongoDB.
* [Basics of Plotting](https://github.com/tspence/data-analysis-class-resources/blob/main/jupyter/Basics%20of%20Plotting.ipynb) - Some basic ways to use matplotlib and pandas to chart certain types of graphs.  This is a very basic tutorial - there's lots of advanced graphing lessons you can find online to build on these ideas!
* [Basics of Storing Data](https://github.com/tspence/data-analysis-class-resources/blob/main/jupyter/Basics%20of%20Storing%20Data.ipynb) - How to store information into MongoDB using single inserts, multiple inserts, and batches.
* [Basics of Web Scraping](https://github.com/tspence/data-analysis-class-resources/blob/main/jupyter/Basics%20of%20Web%20Scraping.ipynb) - An example of how to fetch a web page using urllib3, how to fetch multiple pages in a range, and how to search within that page using regular expressions.  Also includes some examples of how to format output and how to display thumbnails within a Jupyter notebook using Pandas dataframes.

## Example Applications

* [IMDB Scraper](https://github.com/tspence/data-analysis-class-resources/blob/main/jupyter/IMDB%20Scraper.ipynb) - An example program that fetches data from IMDB and stores it into a MongoDB database.
* [IMDB Mongo Scraper](https://github.com/tspence/data-analysis-class-resources/blob/main/jupyter/IMDB%20Mongo%20Scraper.ipynb) - The same program, but this time the data is stored in a MongoDB database.  Includes extra logic to make the scraper restartable.
* [Language Processing](https://github.com/tspence/data-analysis-class-resources/blob/main/jupyter/Language%20Processing%20Example.ipynb) - An example tutorial demonstrating sentiment analysis.
* [Topic Classification](https://github.com/tspence/data-analysis-class-resources/blob/main/jupyter/Topic%20Classification%20Example.ipynb) - A small example that demonstrates usage of [DistilRoBERTa-based](https://huggingface.co/j-hartmann/emotion-english-distilroberta-base), a neural network program that classifies text based on concepts of emotions.