# How to do it?

## Redirect and execute a func

HTML

```html
<a href="/Checkout?amazonPay" id="mcAmazonPayDropDownButtonInit">
```

JS

```js
// location.search shows everithing after "?". ex. mysite.com/Checkout?amazonPay > ?amazonPay
var shopingCardRedirect == != => location.search; 
        console.log('location.search ' + location.search);
        if (shopingCardRedirect === '?amazonPay') {
            $('#mcAmazonPayButtonInit').trigger('click');
        }
```



### A Practical guide to ES6 modules

https://www.freecodecamp.org/news/how-to-use-es6-modules-and-why-theyre-important-a9b20b480773/

The old fashioned way was to load the scripts right before the `</body>` element.

```html
<!DOCTYPE html>
<head>
</head>
<body>
  
  <!--HTML content goes here-->
  
  <script src="js/jquery.js"></script>
  <script src="js/script2.js"></script>
  <script src="js/script3.js"></script>
  <script src="js/script4.js"></script>
</body>
</html>
```

But in the long run, the number of scripts adds up and we may end up with 10+ scripts while trying to maintain version and dependency conflicts.





#### The import and export statements

The **export** keyword is used when we want to make something available somewhere, and the **import** is used to access what export has made available.

The thumb rule is, **in order to import something, you first need to export it**.
And what can we actually export?

* A variable
* An object literal
* A class
* A function
* ++

To simplify the example as shown above, we can wrap all scripts one file.



```js
import { jquery } from './js/jquery.js';
import { script2 } from './js/script2.js';
import { script3 } from './js/script3.js';
import { script4 } from './js/script4.js';
```



And then just load **app.js** script in our **index.html**. But first, in order to make it work, we need to use **type="module"** (source) so that we can use the **import** and **export** for working with modules.

```js
<!DOCTYPE html>
<head>
</head>
<body>
  
  <!--HTML content goes here-->
  
  <script type="module" src="js/app.js"></script>
</body>
</html>
```



##### important

**Note:** I would not recommend having all scripts loaded in one file such as `app.js`, except the ones that require it.



#### How ES6 modules work

What is the difference between a module and a component? A module is a collection of small independent units (components) that we can reuse in our application.



##### **What’s the purpose?**

- Encapsulate behaviour
- Easy to work with
- Easy to maintain
- Easy to scale





#### *make a export

at the end of the js add 

export default <function>;



for example

```js
export default lozad;
```

 the go to the main.js


<div class="adsf""> 
<iframe width="560" height="315" src="https://www.youtube.com/embed/HTXTVfBCeSY" frameborder="0" allow="accelerometer; clipboard-write; encrypted-media; gyroscope; picture-in-picture" allowfullscreen></iframe>

</div>



tell to the broswer that we are use **modules** in that js file 

and use the **defer** att to load it 

![image-20200502185509207](js.assets/image-20200502185509207.png)

Note: defer should be as default





### Validation