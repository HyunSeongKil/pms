package dev.hyunlab.pms.persistence;

import java.util.List;
import java.util.Optional;

import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;

import dev.hyunlab.pms.model.DayEvent;

@Repository
public interface DayEventRepository extends JpaRepository<DayEvent, Long> {
    Optional<DayEvent> findById(Long id);

    List<DayEvent> findAll();


    /**
     * @see https://yonguri.tistory.com/122
     * @param de
     * @return
     */
	List<DayEvent> findByDeStartingWith(String de);
}
