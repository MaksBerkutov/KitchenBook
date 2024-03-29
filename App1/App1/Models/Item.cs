﻿using System;
/*
 Создать приложение «Книга кулинарных рецептов».
Основная задача проекта – создание и использования кулинарных рецептов.
Интерфейс приложения должен предоставлять такие возможности:
■ Создавать кулинарный рецепт. Рецепт может содержать информацию о
компонентах рецепта, изображения, пошаговую инструкцию по созданию
блюда и т. д. При создании рецепта нужно сохранять: название рецепта,
тип рецепта (первые блюда, салаты и т. д.), название кухни, к которой относится данный рецепт (французская, китайская и т. д.).
■ Удаление/изменение рецепта.
■ Каталог рецептов по категориям. Например, первые блюда, салаты и т. д.
■ Экспорт конкретного рецепта в файл формата: .doc, .docx, .pdf.
■ Поиск рецептов по набору параметров. Например, по названию, по компонентам, по названию кухни и т. д.
 
 */
namespace App1.Models
{
    public class Item
    {
        public string Id { get; set; }//ID  //0
        public string Name { get; set; }//1
        public string Category { get; set; }//2
        public string Type { get; set; }//3
        public string NameKitchen { get; set; }//4
        public string HTMLText { get; set; }// ==longtext //5


       
    }
}