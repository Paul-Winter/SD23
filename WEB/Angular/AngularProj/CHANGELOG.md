В этом файле объясняется, как Visual Studio создала проект.

Для создания этого проекта использовались следующие средства:
- Angular CLI (ng)

Для создания этого проекта были использованы следующие шаги:
- Создайте проект Angular с помощью ng: `ng new "AngularProj" --defaults --skip-install --skip-git --no-standalone `.
- Обновите angular.json с портом.
- Создание файла проекта (`AngularProj.esproj`).
- Создайте `launch.json`, чтобы включить отладку.
- Обновите package.json для добавления `jest-editor-support`.
- Обновите сценарий `start` в `package.json`, чтобы указать узел.
- Добавьте `karma.conf.js` для модульных тестов.
- Обновите `angular.json`, чтобы указать на `karma.conf.js`.
- Добавить проект в решение.
- Запишите этот файл.
