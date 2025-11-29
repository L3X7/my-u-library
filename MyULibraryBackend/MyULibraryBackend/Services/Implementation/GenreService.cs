using AutoMapper;
using MyULibraryBackend.Dtos;
using MyULibraryBackend.Entities.Models;
using MyULibraryBackend.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyULibraryBackend.Services.Implementation
{
    public class GenreService : IGenreService
    {
        private readonly IGenreRepository _genreRepository;
        private readonly IMapper _mp;

        public GenreService(IGenreRepository genreRepository, IMapper mp)
        {
            _genreRepository = genreRepository;
            _mp = mp;
        }

        public async Task CreateGenreAsync(CreateGenreDto request)
        {
            var existingGenre = _genreRepository.GetByNameAsync(request.GenreName);
            if (existingGenre != null)
                throw new Exception($"Genre '{request.GenreName}' is already exist.");
            var newGenre = new Genre
            {
                GenreName = request.GenreName,
            };
            await _genreRepository.AddAsync(newGenre);
            await _genreRepository.SaveChangesAsync();
        }

        public async Task DeleteGenreAsync(long id)
        {
            var genre = await _genreRepository.GetByIdAsync(id);
            if(genre == null)
                throw new KeyNotFoundException($"Genre with ID {id} not found.");
            _genreRepository.Delete(genre);
            await _genreRepository.SaveChangesAsync();
        }

        public async Task<List<GenreDto>> GetAllGenresAsync()
        {
            var genres = await _genreRepository.GetAllAsync();
            return _mp.Map<List<GenreDto>>(genres);
        }

        public async Task<GenreDto> GetGenreByIdAsync(long id)
        {
            var genre = await _genreRepository.GetByIdAsync(id);
            return _mp.Map<GenreDto>(genre);
        }

        public async Task<GenreDto> GetGenreByNameAsync(string genreNane)
        {
            var genre = await _genreRepository.GetByNameAsync(genreNane);
            return _mp.Map<GenreDto>(genre);
        }

        public async Task UpdateGenreAsync(long id, UpdateGenreDto request)
        {
            var genre = await _genreRepository.GetByIdAsync(id);
            if (genre == null)
                throw new KeyNotFoundException($"Genre with ID {id} not found.");
            genre.GenreName = request.GenreName;
            await _genreRepository.SaveChangesAsync();
        }
    }
}
