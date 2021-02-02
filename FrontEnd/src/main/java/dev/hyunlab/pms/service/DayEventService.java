package dev.hyunlab.pms.service;

import java.util.List;
import java.util.Optional;

import org.springframework.beans.factory.annotation.Autowired;

import dev.hyunlab.pms.model.DayEvent;
import dev.hyunlab.pms.persistence.DayEventRepository;

public interface DayEventService {
    Optional<DayEvent> findById(Long id);

    List<DayEvent> findAll();

    List<DayEvent> findByDeStartingWith(String de);

	void save(DayEvent model);
}
