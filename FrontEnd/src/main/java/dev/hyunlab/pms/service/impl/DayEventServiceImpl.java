package dev.hyunlab.pms.service.impl;

import java.util.List;
import java.util.Optional;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import dev.hyunlab.pms.model.DayEvent;
import dev.hyunlab.pms.persistence.DayEventRepository;
import dev.hyunlab.pms.service.DayEventService;

@Service
public class DayEventServiceImpl implements DayEventService {

    @Autowired
    private DayEventRepository repo;

    @Override
    public Optional<DayEvent> findById(Long id) {
        return repo.findById(id);
    }

    @Override
    public List<DayEvent> findAll() {
        return repo.findAll();
    }

    @Override
    public List<DayEvent> findByDeStartingWith(String de) {
        return repo.findByDeStartingWith(de);
    }

    @Override
    public void save(DayEvent entity) {
        repo.save(entity);
    }
    
}
